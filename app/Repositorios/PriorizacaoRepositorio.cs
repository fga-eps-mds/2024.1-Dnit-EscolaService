using api;
using api.CustoLogistico;
using api.Fatores;
using app.Entidades;
using app.Repositorios.Interfaces;
using app.Services;
using Microsoft.EntityFrameworkCore;

namespace app.Repositorios
{
    public class PriorizacaoRepositorio : IPriorizacaoRepositorio
    {
        private readonly AppDbContext dbContext;

        public PriorizacaoRepositorio(
            AppDbContext dbContext
        )
        {
            this.dbContext = dbContext;
        }

        public async Task<FatorPriorizacao>  ObterFatorPrioriPorIdAsync(Guid prioriId)
        {
            return await dbContext.FatorPriorizacoes.FirstOrDefaultAsync(c => c.Id == prioriId) ?? throw new ApiException(ErrorCodes.FatorNaoEncontrado); 
        }

        public async Task<FatorCondicao>  ObterFatorCondiPorIdAsync(Guid condicaoId)
        {
            return await dbContext.FatorCondicoes.FirstOrDefaultAsync(c => c.Id == condicaoId) ?? throw new ApiException(ErrorCodes.FatorNaoEncontrado);
        }

        public async Task<FatorEscola> ObterFatorEscolaPorIdAsync(Guid escolaId)
        {
            return await dbContext.FatorEscolas.FirstOrDefaultAsync(c => c.EscolaId == escolaId) ?? throw new ApiException(ErrorCodes.FatorNaoEncontrado);
        }

        public async Task DeletarFatorId(Guid Id)
        {
            FatorPriorizacao item = await ObterFatorPrioriPorIdAsync(Id);
            dbContext.FatorPriorizacoes.Remove(item);
            dbContext.SaveChanges();
        }

        public async Task<List<FatorPriorizacao>> ListarFatoresAsync()
        {
            return await dbContext.FatorPriorizacoes
                .OrderBy(c => c.Id)
                .ToListAsync();
        }

        public async Task<List<CustoLogistico>> ListarCustosLogisticosAsync()
        {
            return await dbContext.CustosLogisticos
                .OrderBy(c => c.Custo)
                .ToListAsync();
        }

        public FatorCondicao AdicionarFatorCondicao(FatorCondicaoModel fatorCondicao)
        {
            FatorCondicao fator = new FatorCondicao
            {
                Id = Guid.NewGuid(),
                Propriedade = fatorCondicao.Propriedade,
                Operador = fatorCondicao.Operador,
                Valor = fatorCondicao.Valor,
                FatorPriorizacaoId = (Guid)fatorCondicao.FatorPriorizacaoId,
            };

            dbContext.FatorCondicoes.Add(fator);
            return fator;
        }

        public FatorPriorizacao AdicionarFatorPriorizacao(FatorPrioriModel novoFator)
        {
            FatorPriorizacao item = new FatorPriorizacao
            {
                Id = Guid.NewGuid(),
                Nome = novoFator.Nome,
                Ativo = novoFator.Ativo,
                Peso = novoFator.Peso,
                Primario = novoFator.Primario
            };

            dbContext.FatorPriorizacoes.Add(item);
            return item;
        }

        public async Task<List<CustoLogistico>> EditarCustosLogisticos(List<CustoLogisticoItem> custoItems)
        {
            var custosAtualizados = new List<CustoLogistico>();

            foreach (var item in custoItems)
            {
                var custoLogistico = await dbContext.CustosLogisticos.FirstOrDefaultAsync(c => c.Custo == item.Custo);

                if (custoLogistico != null)
                {
                    custoLogistico.RaioMax = item.RaioMax;
                    custoLogistico.RaioMin = item.RaioMin;
                    custoLogistico.Valor = item.Valor;
                    custosAtualizados.Add(custoLogistico);
                }
            }

            return custosAtualizados;
        }
    }
}
