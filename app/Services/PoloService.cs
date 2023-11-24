using app.Entidades;
using app.Repositorios.Interfaces;
using app.Services.Interfaces;

namespace app.Services;

public class PoloService : IPoloService
{
    private readonly IPoloRepositorio _poloRepositorio;

    public PoloService(IPoloRepositorio poloRepositorio)
    {
        this._poloRepositorio = poloRepositorio;
    }

    public async Task<Polo> ObterPorIdAsync(int id)
    {
        return await _poloRepositorio.ObterPorIdAsync(id);
    }
}
