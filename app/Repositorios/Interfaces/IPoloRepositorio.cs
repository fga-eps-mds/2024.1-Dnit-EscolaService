using api;
using api.Polos;
using app.Entidades;

namespace app.Repositorios.Interfaces;

public interface IPoloRepositorio
{
    Task<Polo> ObterPorIdAsync(int id);
    Task<List<Polo>> ListarAsync();
    Polo Criar(CadastroPoloDTO poloDto, Municipio municipio);
    Task<ListaPaginada<Polo>> ListarPaginadaAsync(PesquisaPoloFiltro filtro);
}
