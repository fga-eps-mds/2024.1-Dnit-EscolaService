using api.CustoLogistico;
using app.Services.Interfaces;
using app.Repositorios;
using app.Repositorios.Interfaces;
using app.Entidades;

namespace app.Services
{
    public class PriorizacaoService : IPriorizacaoService
    {
        private readonly AppDbContext dbContext;
        IPriorizacaoRepositorio priorizacaoRepositorio;
        ModelConverter modelConverter;
        
        public PriorizacaoService(
            AppDbContext dbContext,
            IPriorizacaoRepositorio priorizacaoRepositorio,
            ModelConverter modelConverter
        )
        {
            this.dbContext = dbContext;
            this.priorizacaoRepositorio = priorizacaoRepositorio;
            this.modelConverter = modelConverter;
        }

        public async Task<List<CustoLogisticoItem>> ListarCustosLogisticos()
        {
            var items = await priorizacaoRepositorio.ListarCustosLogisticosAsync();
            return items.ConvertAll(modelConverter.ToModel);
        }
        public async Task<List<CustoLogisticoItem>> EditarCustosLogisticos(CustoLogisticoItem[] custoItems)
        {
            for (int i = 1; i < custoItems.Length; i++)
            {
                if (custoItems[i].RaioMin != custoItems[i - 1].RaioMax)
                {
                    throw new InvalidOperationException("Operação Inválida: O RaioMin deve ser igual ao RaioMax anterior");
                }
            }

            for (int i = 0; i < custoItems.Length; i++)
            {
                if (custoItems[i].RaioMax != null && custoItems[i].RaioMin >= custoItems[i].RaioMax)
                {
                    throw new InvalidOperationException("Operação Inválida: O RaioMin deve ser menor que o RaioMax");
                }
            }

            var custosAtualizados = await priorizacaoRepositorio.EditarCustosLogisticos(custoItems);
            await dbContext.SaveChangesAsync();

            return custosAtualizados.ConvertAll(modelConverter.ToModel);
        }
    }
}
