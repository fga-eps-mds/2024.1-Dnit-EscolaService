using app.Entidades;

namespace app.Repositorios.Interfaces
{
    public interface IPriorizacaoRepositorio
    {
        Task<List<CustoLogistico>> ListarCustosLogisticosAsync();
    }
}
