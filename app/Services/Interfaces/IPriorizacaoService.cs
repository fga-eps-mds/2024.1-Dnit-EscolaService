using api.CustoLogistico;

namespace app.Services.Interfaces
{
    public interface IPriorizacaoService
    {
        Task<List<CustoLogisticoItem>> ListarCustosLogisticos();
    }
}
