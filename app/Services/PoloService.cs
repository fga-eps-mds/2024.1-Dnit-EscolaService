using api;
using api.Polos;
using app.Entidades;
using app.Repositorios.Interfaces;
using app.Services.Interfaces;

namespace app.Services;

public class PoloService : IPoloService
{
    private readonly IPoloRepositorio _poloRepositorio;
    private readonly IMunicipioRepositorio _municipioRepositorio;
    private readonly AppDbContext _dbContext;
    private readonly ModelConverter _modelConverter;

    public PoloService(IPoloRepositorio poloRepositorio, IMunicipioRepositorio municipioRepositorio, AppDbContext dbContext, ModelConverter modelConverter)
    {
        this._poloRepositorio = poloRepositorio;
        _municipioRepositorio = municipioRepositorio;
        _dbContext = dbContext;
        _modelConverter = modelConverter;
    }

    public async Task<Polo> ObterPorIdAsync(int id)
    {
        return await _poloRepositorio.ObterPorIdAsync(id);
    }

    public async Task CadastrarAsync(CadastroPoloDTO poloDto)
    {
        var municipioId = poloDto.MunicipioId;
        var municipio = await _municipioRepositorio.ObterPorIdAsync(municipioId);

        _poloRepositorio.Criar(poloDto, municipio);

        await _dbContext.SaveChangesAsync();
    }

    public async Task<ListaPaginada<PoloModel>> ListarPaginadaAsync(PesquisaPoloFiltro filtro)
    {
        var polos = await _poloRepositorio.ListarPaginadaAsync(filtro);
        var poloModels = polos.Items.ConvertAll(_modelConverter.ToModel);
        return new ListaPaginada<PoloModel>(poloModels, polos.Pagina, polos.ItemsPorPagina, polos.Total);
    }

    public async Task AtualizarAsync(Polo poloExistente, CadastroPoloDTO poloDto)
    {
        poloExistente.Endereco = poloDto.Endereco;
        poloExistente.Cep = poloDto.Cep;
        poloExistente.Latitude = poloDto.Latitude;
        poloExistente.Longitude = poloDto.Longitude;
        poloExistente.Nome = poloDto.Nome;

        await _dbContext.SaveChangesAsync();
    }

    public async Task ExcluirAsync(Polo poloExistente)
    {
        _poloRepositorio.Excluir(poloExistente);

        await _dbContext.SaveChangesAsync();
    }
}
