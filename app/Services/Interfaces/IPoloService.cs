using api;
using api.Polos;
using app.Entidades;

namespace app.Services.Interfaces;

public interface IPoloService
{
    Task<Polo> ObterPorIdAsync(int id);
    Task CadastrarAsync(CadastroPoloDTO poloDto);
    Task<ListaPaginada<PoloModel>> ListarPaginadaAsync(PesquisaPoloFiltro filtro);
    Task AtualizarAsync(Polo poloExistente, CadastroPoloDTO poloDto);
    Task ExcluirAsync(Polo poloExistente);
}
