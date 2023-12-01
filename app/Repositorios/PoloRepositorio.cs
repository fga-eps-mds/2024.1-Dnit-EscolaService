using api;
using api.Polos;
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

    public Polo Criar(CadastroPoloDTO poloDto, Municipio municipio)
    {
        var polo = new Polo
        {
            Id = poloDto.Id,
            Nome = poloDto.Nome,
            Municipio = municipio,
            Longitude = poloDto.Longitude,
            Latitude = poloDto.Latitude,
            Uf = (UF)poloDto.IdUf,
            Endereco = poloDto.Endereco,
            Cep = poloDto.Cep,
        };
        dbContext.Add(polo);
        return polo;
    }
}
