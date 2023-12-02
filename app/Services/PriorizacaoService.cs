using api.CustoLogistico;
using app.Services.Interfaces;
using app.Repositorios;
using app.Repositorios.Interfaces;
using app.Entidades;
using api.Fatores;

namespace app.Services
{
    public class PriorizacaoService : IPriorizacaoService
    {
        private readonly AppDbContext dbContext;
        private readonly IPriorizacaoRepositorio priorizacaoRepositorio;
        private readonly ModelConverter modelConverter;
        
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

        public async Task<List<FatorPrioriModel>> ListarFatores()
        {
            var items = await priorizacaoRepositorio.ListarFatoresAsync();
            return items.ConvertAll(modelConverter.ToModel);
        }
        public async Task<List<CustoLogisticoItem>> EditarCustosLogisticos(List<CustoLogisticoItem> custoItems)
        {
            if (custoItems.Count != 4)
            {
                throw new InvalidOperationException("Operação Inválida: Deve conter as 4 categorias de custo logístico");
            }

            var sortedCustoItems = custoItems.OrderBy(item => item.Custo).ToList();
            int[] custosPermitidos = { 1, 2, 3, 4 };

            if (sortedCustoItems.Distinct().Count() == custoItems.Count)
            {
                if (sortedCustoItems.Select(item => item.Custo).All(c => custosPermitidos.Contains(c)))
                {
                    throw new InvalidOperationException("Operação Inválida: Deve conter categorias de 1 a 4");
                }
            }
            else
            {
                throw new InvalidOperationException("Operação Inválida: Categorias de custo logístico repetidas");
            }

            for (int i = 1; i < custoItems.Count; i++)
            {
                if (sortedCustoItems[i].RaioMin != sortedCustoItems[i - 1].RaioMax)
                {
                    throw new InvalidOperationException("Operação Inválida: O RaioMin deve ser igual ao RaioMax anterior");
                }
            }

            if (sortedCustoItems.Any(item => item.RaioMax != null && item.RaioMin >= item.RaioMax))
            {
                throw new InvalidOperationException("Operação Inválida: O RaioMin deve ser menor que o RaioMax");
            }

            var custosAtualizados = await priorizacaoRepositorio.EditarCustosLogisticos(sortedCustoItems);
            await dbContext.SaveChangesAsync();

            return custosAtualizados.ConvertAll(modelConverter.ToModel);
        }
    }
}
