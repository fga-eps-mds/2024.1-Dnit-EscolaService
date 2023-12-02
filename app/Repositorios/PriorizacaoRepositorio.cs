using api;
using api.CustoLogistico;
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
            return dbContext.FatorPriorizacoes.FirstOrDefault(c => c.Id == prioriId) ?? throw new ApiException(ErrorCodes.FatorNaoEncontrado); 

            
        }
        public async Task<FatorCondicao>  ObterFatorCondiPorIdAsync(Guid condicaoId)
        {
            return dbContext.FatorCondicoes.FirstOrDefault(c => c.Id == condicaoId) ?? throw new ApiException(ErrorCodes.FatorNaoEncontrado);
        }
        public async Task<FatorEscola>  ObterFatorEscolaPorIdAsync(Guid escolaId)
        {
            return dbContext.FatorEscolas.FirstOrDefault(c => c.EscolaId == escolaId) ?? throw new ApiException(ErrorCodes.FatorNaoEncontrado);
        }

        public async Task<List<CustoLogistico>> ListarCustosLogisticosAsync()
        {
            return await dbContext.CustosLogisticos
                .OrderBy(c => c.Custo)
                .ToListAsync();
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
