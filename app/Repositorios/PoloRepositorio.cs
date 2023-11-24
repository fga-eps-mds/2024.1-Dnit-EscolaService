using api;
using app.Entidades;
using app.Repositorios.Interfaces;
using app.Services;
using Microsoft.EntityFrameworkCore;

namespace app.Repositorios;

public class PoloRepositorio : IPoloRepositorio
{

    private readonly AppDbContext dbContext;

    public PoloRepositorio(AppDbContext dbContext)
    {
        this.dbContext = dbContext;
    }

    public async Task<Polo> ObterPorIdAsync(int id)
    {
        return await dbContext.Polos.FirstOrDefaultAsync(x => x.Id == id)
            ?? throw new ApiException(ErrorCodes.PoloNaoEncontrado);
    }

    public async Task<List<Polo>> ListarAsync()
    {
        return await dbContext.Polos.ToListAsync();
    }
}
