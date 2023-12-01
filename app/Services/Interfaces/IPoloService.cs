using api.Polos;
using app.Entidades;

namespace app.Services.Interfaces;

public interface IPoloService
{
    Task<Polo> ObterPorIdAsync(int id);
    Task CadastrarAsync(CadastroPoloDTO poloDto);
}
