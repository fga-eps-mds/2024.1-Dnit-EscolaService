using api.CustoLogistico;
using api.Fatores;

namespace app.Services.Interfaces
{
    public interface IPriorizacaoService
    {
        Task <FatorPrioriModel> VisualizarFatorId(Guid Id);
        Task<List<FatorPrioriModel>> ListarFatores();
        Task<List<CustoLogisticoItem>> ListarCustosLogisticos();
        Task<List<CustoLogisticoItem>> EditarCustosLogisticos(List<CustoLogisticoItem> custoItems);
    }
}
