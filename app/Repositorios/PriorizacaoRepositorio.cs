using api.CustoLogistico;
using app.Entidades;
using app.Repositorios.Interfaces;
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
