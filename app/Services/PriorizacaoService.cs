using api.CustoLogistico;
using app.Services.Interfaces;
using app.Repositorios;
using app.Repositorios.Interfaces;

namespace app.Services
{
    public class PriorizacaoService : IPriorizacaoService
    {
        IPriorizacaoRepositorio priorizacaoRepositorio;
        ModelConverter modelConverter;
        
        public PriorizacaoService(
            IPriorizacaoRepositorio priorizacaoRepositorio,
            ModelConverter modelConverter
        )
        {
            this.priorizacaoRepositorio = priorizacaoRepositorio;
            this.modelConverter = modelConverter;
        }
        public async Task<List<CustoLogisticoItem>> ListarCustosLogisticos()
        {
            var items = await priorizacaoRepositorio.ListarCustosLogisticosAsync();
            return items.ConvertAll(modelConverter.ToModel);
        }
    }
}
