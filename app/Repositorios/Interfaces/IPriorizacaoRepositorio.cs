using api.CustoLogistico;
using app.Entidades;

namespace app.Repositorios.Interfaces
{
    public interface IPriorizacaoRepositorio
    {
        Task<List<CustoLogistico>> ListarCustosLogisticosAsync();
        Task<List<CustoLogistico>> EditarCustosLogisticos(CustoLogisticoItem[] custoItems);
    }
}
