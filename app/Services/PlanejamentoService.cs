using System.Numerics;
using api;
using api.Escolas;
using api.Planejamento;
using app.Entidades;
using app.Repositorios.Interfaces;
using EnumsNET;
using Microsoft.VisualBasic;
using service.Interfaces;

namespace app.Services
{
    public class PlanejamentoService : IPlanejamentoService
    {
        private readonly IPlanejamentoRepositorio planejamentoRepositorio;
        private readonly IRanqueService ranqueService;
        private readonly ModelConverter modelConverter;
        private readonly AppDbContext dbContext;

        public PlanejamentoService
        (
            IPlanejamentoRepositorio planejamentoRepositorio,
            ModelConverter modelConverter,
            IRanqueService ranqueService,
            AppDbContext dbContext
        )
        {
            this.planejamentoRepositorio = planejamentoRepositorio;
            this.modelConverter = modelConverter;
            this.dbContext = dbContext;
            this.ranqueService = ranqueService;
        }

        // Implementar os metodos da inteface
        //Obtém planejamento macro pelo ID
        public async Task<PlanejamentoMacro> ObterPlanejamentoMacroAsync(Guid id)
        {
            return await planejamentoRepositorio.ObterPlanejamentoMacroAsync(id);
        }

            
        public async Task ExcluirPlanejamentoMacro(Guid id)
        {
            var planejamento = await planejamentoRepositorio.ObterPlanejamentoMacroAsync(id);
            planejamento.Escolas.ForEach(e => planejamentoRepositorio.ExcluirPlanejamentoMacroEscola(e));
            planejamentoRepositorio.ExcluirPlanejamentoMacro(planejamento);
            await dbContext.SaveChangesAsync();
        }
        

        public async Task<PlanejamentoMacro> GerarRecomendacaoDePlanejamento(PlanejamentoMacroDTO planejamento)
        {
            //algoritmo de recomendação fake a fim de teste
            //retorna uma recomendação apenas com as primeiras escolas do ranking separadas nos meses
            
            var q = planejamento.QuantidadeAcoes;
            // var n = (int) Math.Ceiling(q + q * 0.35);

            var filtro = new PesquisaEscolaFiltro()
            {
                TamanhoPagina = q
            };

            var escolas = await ranqueService.ListarEscolasUltimoRanqueAsync(filtro);
            var numeroMeses = Math.Abs(planejamento.MesFim - planejamento.MesInicio) + 1;
            var acoesPorMes = (int) Math.Ceiling((double) q / numeroMeses);

            var lista = new List<PlanejamentoMacroEscola>();

            int i = 0, mes = (int) planejamento.MesInicio;
            foreach(var e in escolas.Items)
            {
                if(i == acoesPorMes)
                {
                    i = 0;
                    mes = mes == 12 ? 1 : mes + 1;
                }

                var planejamentoMacroEscola = new PlanejamentoMacroEscola()
                {
                    Mes = (Mes) mes,
                    Ano = planejamento.AnoInicio,
                    EscolaId = e.Escola.Id    
                };

                lista.Add(planejamentoMacroEscola);
                i++;
            }

            var planejamentoMacroGerado = new PlanejamentoMacro()
            {
                Nome = planejamento.Nome,
                Responsavel = planejamento.Responsavel,
                MesInicio = planejamento.MesInicio,
                MesFim = planejamento.MesFim,
                AnoInicio = planejamento.AnoInicio,
                AnoFim = planejamento.AnoFim,
                QuantidadeAcoes = planejamento.QuantidadeAcoes,
                Escolas = lista                
            };

            //algoritmo de recomendação real:    
            //pegar um vetor V com as n primeiras escolas do ranking
            //normalizar V para objetos com {Id, Ups, CustoLogistico}
            //implemetar a função-custo como: [1]
            //rodar uma otimização por enxame de partículas ou um algoritmo genético
            //usar q dimensões num S = [0, n]
            //captar o vetor R com o resultado da otimização
            //R é composto pelos indices das escolas escolhidas em V
            //dividir q igualmente pela quantidade de meses
            //tentar agrupar por UF as escolas selecionadas no mesmo Mês
            //Montar um objeto PlanejamentoMacro com o resultado

            /* [1]
            rascunho de função-custo:
        
                X -> vetor q-dimensional
                custo = 0;
                for(int i = 0, i < q, i++)
                {
                    custo += -V[X[i]].Ups + V[X[i]].custologistico
                }
            */

            return planejamentoMacroGerado;
        }

        public PlanejamentoMacro CriarPlanejamentoMacro(PlanejamentoMacro planejamento)
        {
            var planejamentoMacro = planejamentoRepositorio.RegistrarPlanejamentoMacro(planejamento);
            dbContext.SaveChanges();
            return planejamentoMacro;
        }

        public async Task<PlanejamentoMacro> EditarPlanejamentoMacro(PlanejamentoMacro planejamento, PlanejamentoMacroDTO dto)
        {
            var planejamentoMacroAtualizar = await planejamentoRepositorio.ObterPlanejamentoMacroAsync(planejamento.Id) ?? throw new KeyNotFoundException("Planejamento não encontrado");

            planejamentoMacroAtualizar.Nome = dto.Nome;
            planejamentoMacroAtualizar.Responsavel = dto.Responsavel;
            planejamentoMacroAtualizar.MesInicio = dto.MesInicio;
            planejamentoMacroAtualizar.MesFim = dto.MesFim;
            planejamentoMacroAtualizar.AnoInicio = dto.AnoInicio;
            planejamentoMacroAtualizar.AnoFim = dto.AnoFim;
            planejamentoMacroAtualizar.QuantidadeAcoes = dto.QuantidadeAcoes;

            await dbContext.SaveChangesAsync();

            return planejamentoMacroAtualizar;
        }

        public async Task<ListaPaginada<PlanejamentoMacroDetalhadoModel>> ListarPaginadaAsync(PesquisaPlanejamentoFiltro filtro)
        {
            var planejamentos = await planejamentoRepositorio.ListarPaginadaAsync(filtro);
            var planejamentosCorretos = planejamentos.Items.ConvertAll(modelConverter.ToModel);
            return new ListaPaginada<PlanejamentoMacroDetalhadoModel>(planejamentosCorretos, planejamentos.Pagina, planejamentos.ItemsPorPagina, planejamentos.Total);
        }

        // public async Task EditarPlanejamentoMacroAsync(Guid id, PlanejamentoMacroplanejamento pmplanejamento)
        // {
        //     var planejamento = await planejamentoRepositorio.ObterPlanejamentoMacroAsync(id);

        //     planejamento!.Nome = pmplanejamento.Nome;
        //     planejamento!.Responsavel = pmplanejamento.Responsavel;
        //     planejamento!.MesInicio = pmplanejamento.MesInicio;
        //     planejamento!.MesFim = pmplanejamento.MesFim;  
        //     planejamento!.AnoInicio = pmplanejamento.AnoFim;
        //     planejamento!.QuantidadeAcoes = pmplanejamento.QuantidadeAcoes;

        //     await dbContext.SaveChangesAsync();
        // }
    }
}