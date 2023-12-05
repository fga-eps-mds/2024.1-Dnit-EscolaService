using api.CustoLogistico;
using app.Entidades;

namespace app.Repositorios.Interfaces
{
    public interface IPriorizacaoRepositorio
    {
        Task DeletarFatorId(Guid Id);
        Task<List<FatorPriorizacao>> ListarFatoresAsync();
        Task<FatorPriorizacao> ObterFatorPrioriPorIdAsync(Guid prioriId);
        Task<FatorCondicao> ObterFatorCondiPorIdAsync(Guid condicaoId);
        Task<FatorEscola> ObterFatorEscolaPorIdAsync(Guid escolaId);
        Task<List<CustoLogistico>> ListarCustosLogisticosAsync();
        Task<List<CustoLogistico>> EditarCustosLogisticos(List<CustoLogisticoItem> custoItems);
    }
}
