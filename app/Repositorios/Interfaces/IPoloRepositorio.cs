using api;
using app.Entidades;

namespace app.Repositorios.Interfaces;

public interface IPoloRepositorio
{
    Task<Polo> ObterPorIdAsync(int id);
    Task<List<Polo>> ListarAsync();
}
