using api;
using api.Escolas;
using app.Entidades;
using app.Repositorios.Interfaces;
using app.Services.Interfaces;
using Hangfire;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using service.Interfaces;

namespace app.Services
{
    public class CalcularRanqueJob : ICalcularRanqueJob
    {
        private readonly AppDbContext dbContext;
        private readonly IBackgroundJobClient jobClient;
        private readonly IEscolaRepositorio escolaRepositorio;

        public CalcularRanqueJob(
            AppDbContext dbContext,
            IBackgroundJobClient jobClient,
            IEscolaRepositorio escolaRepositorio
        )
        {
            this.dbContext = dbContext;
            this.escolaRepositorio = escolaRepositorio;
            this.jobClient = jobClient;
        }

        [MaximumConcurrentExecutions(1)]
        public async Task ExecutarAsync(int novoRanqueId, int timeoutMinutos)
        {
            var TamanhoBatelada = 50;

            var totalEscolas = await dbContext.Escolas.CountAsync();
            var filtro = new PesquisaEscolaFiltro { TamanhoPagina = TamanhoBatelada};
            var totalPaginas = Math.Ceiling((double)totalEscolas / TamanhoBatelada);

            var ranque = dbContext.Ranques.Where(r => r.Id == novoRanqueId).First();

            for (int pagina = 1; pagina <= totalPaginas; pagina++)
            {
                filtro.Pagina = pagina;
                var lista = await escolaRepositorio.ListarPaginadaAsync(filtro);
                
                CalcularPontuacaoEscolas(lista.Items, novoRanqueId);
                
                ranque.BateladasEmProgresso = (int)totalPaginas - pagina;
                
                dbContext.Update(ranque);
                dbContext.SaveChanges();
            }
        }

        private void CalcularPontuacaoEscolas(List<Escola> escolas, int ranqueId)
        {
            var upsMax = dbContext.Escolas.Max(e => e.Ups);

            var fatorUps = dbContext.FatorPriorizacoes
                .Where(f => f.Nome.Contains("UPS") && f.Ativo && f.Primario && f.DeleteTime == null).First();

            var fatorCustoLogisticos = dbContext.FatorPriorizacoes
                .Where(f => f.Nome.Contains("Custo Logistico") && f.Ativo && f.Primario && f.DeleteTime == null).First();

            var custosLogisticos = dbContext.CustosLogisticos.ToList();

            var outrosFatores = dbContext.FatorPriorizacoes
                .Include(f => f.FatorCondicoes)
                .Where(f => !f.Primario && f.DeleteTime == null && f.Ativo)
                .ToList();

            try
            {
                SalvarFatorRanque(fatorUps, ranqueId);
                SalvarFatorRanque(fatorCustoLogisticos, ranqueId);
                foreach(var fator in outrosFatores)
                {
                    SalvarFatorRanque(fator, ranqueId);
                }
            }
            catch{}
            
            foreach(var escola in escolas)
            {
                // Preenche Ups em fatorEscola
                CalcularESalvarFatorUps(escola, fatorUps, upsMax);

                // Preenche CustoLogistico em fatorEscola
                CalcularESalvarFatorCustoLogistico(escola, fatorCustoLogisticos, custosLogisticos);

                // Preenche Outros Fatores em fatorEscola
                CalcularESalvarOutrosFatores(escola, outrosFatores);

                dbContext.SaveChanges();
            }
        }

        private bool ExisteFatorEscola(FatorPriorizacao fator, Escola escola)
        {
            return dbContext.FatorEscolas.Any(f => f.FatorPriorizacao.Id == fator.Id && f.EscolaId == escola.Id);
        }

        private void CalcularESalvarFatorUps(Escola escola, FatorPriorizacao fatorUps, int UpsMax)
        {
            if (ExisteFatorEscola(fatorUps, escola)) return;

            // valorNormalizado = ((valor - min) / (max - min)) * 100
            // Como min será sempre zero, então 
            // valorNormalizado = (valor / max) * 100
            double valorNormalizado = (double)escola.Ups / UpsMax * 100;

            // Valor de pontucao do ups = valorNormalizado * peso / 100
            double valor = valorNormalizado * fatorUps.Peso / 100;

            var fatorEscola = new FatorEscola
            {
                EscolaId = escola.Id,
                FatorPriorizacaoId = fatorUps.Id,
                Valor = (int)valor
            };
            
            dbContext.Add(fatorEscola);
        }

        private void CalcularESalvarFatorCustoLogistico(Escola escola, FatorPriorizacao fator, List<CustoLogistico> custoLogisticos)
        {
            if (ExisteFatorEscola(fator, escola)) return;

            // Valor de pontuacao do custo logistico = Valor(CL) * peso / 100
            int valor = getCustoLogisticoValue(custoLogisticos, escola.DistanciaPolo);
            
            dbContext.Add(new FatorEscola
                {
                    EscolaId = escola.Id,
                    FatorPriorizacaoId = fator.Id,
                    Valor = valor
                });
        }

        private int getCustoLogisticoValue(List<CustoLogistico> custoLogisticos, double raio)
        {
            foreach(var custo in custoLogisticos)
            {
                if (custo.RaioMax == null)
                {
                    return custo.Valor;
                }

                if (raio >= custo.RaioMin && raio < custo.RaioMax)
                {
                    return custo.Valor;
                }
            }
            return 0;
        }

        private void CalcularESalvarOutrosFatores(Escola escola, List<FatorPriorizacao> fatores)
        {
            foreach(var fator in fatores)
            {
                if (ExisteFatorEscola(fator, escola)) continue;
                
                FatorEscola fatorEscola = new FatorEscola
                {
                    EscolaId = escola.Id,
                    FatorPriorizacaoId = fator.Id,
                    Valor = CalcularCondicoes(escola, fator.FatorCondicoes)
                };

                dbContext.Add(fatorEscola);
            }
        }

        private int CalcularCondicoes(Escola escola, List<FatorCondicao> condicoes)
        {
            var resultadoBooleano = true;
            foreach(var condicao in condicoes)
            {
                var eval = EvaluateCondicao(escola, condicao);
                resultadoBooleano = resultadoBooleano && eval;
                if (!resultadoBooleano) break;
            }

            return resultadoBooleano ? 1 : 0;
        }

        private bool EvaluateCondicao(Escola escola, FatorCondicao condicao)
        {
            var query = dbContext.Escolas.Where(e => e.Id == escola.Id);

            switch (condicao.Propriedade)
            {
                case PropriedadeCondicao.Porte:
                    return query.Any(e => condicao.Valores.Any(v => (Porte)CondicaoValorToInt(v.Valor) == e.Porte));
                
                case PropriedadeCondicao.Situacao:
                    return query.Any(e => condicao.Valores.Any(v => (Situacao)CondicaoValorToInt(v.Valor) == e.Situacao));
                
                case PropriedadeCondicao.Municipio:
                    return query.Any(e => condicao.Valores.Any(v => CondicaoValorToInt(v.Valor) == e.Municipio.Id));
                
                case PropriedadeCondicao.UF:
                    return query.Any(e => condicao.Valores.Any(v => (UF)CondicaoValorToInt(v.Valor) == e.Uf));
                
                case PropriedadeCondicao.Localizacao:
                    return query.Any(e => condicao.Valores.Any(v => (Localizacao)CondicaoValorToInt(v.Valor) == e.Localizacao));

                case PropriedadeCondicao.TotalAlunos:
                    if (condicao.Operador == OperacaoCondicao.GTE)
                    {
                        return query.Any(e => condicao.Valores.Any(v => CondicaoValorToInt(v.Valor) >= e.TotalAlunos));
                    }
                    if (condicao.Operador == OperacaoCondicao.LTE)
                    {
                        return query.Any(e => condicao.Valores.Any(v => CondicaoValorToInt(v.Valor) <= e.TotalAlunos));
                    }
                    break;
                
                case PropriedadeCondicao.EtapaEnsino:
                    return query.Any(e => e.EtapasEnsino.Any(ee => condicao.Valores.Any(v => (EtapaEnsino)CondicaoValorToInt(v.Valor) == ee.EtapaEnsino)));
                
                case PropriedadeCondicao.Rede:
                    return query.Any(e => condicao.Valores.Any(v => (Rede)CondicaoValorToInt(v.Valor) == e.Rede));
            }

            return false;
        }

        private int CondicaoValorToInt(string valor)
        {
            try
            {
                return int.Parse(valor);
            }
            catch
            {
                return -1;
            }
        }

        private void SalvarFatorRanque(FatorPriorizacao fator, int ranqueId)
        {
            var fatorRanque = new FatorRanque
            {
                FatorPriorizacaoId = fator.Id,
                RanqueId = ranqueId
            };
            dbContext.Add(fatorRanque);
        }
    }
}