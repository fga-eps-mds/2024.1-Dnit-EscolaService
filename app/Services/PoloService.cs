using System.Globalization;
using api;
using api.Polos;
using app.Entidades;
using app.Repositorios;
using app.Repositorios.Interfaces;
using app.Services.Interfaces;
using app.util;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace app.Services;

public class PoloService : IPoloService
{
    private readonly IPoloRepositorio _poloRepositorio;
    private readonly IMunicipioRepositorio _municipioRepositorio;
    private readonly EscolaRepositorio _escolaRepositorio;
    private readonly AppDbContext _dbContext;
    private readonly ModelConverter _modelConverter;

    public PoloService(
        IPoloRepositorio poloRepositorio, 
        IMunicipioRepositorio municipioRepositorio, 
        AppDbContext dbContext, 
        ModelConverter modelConverter, 
        EscolaRepositorio escolaRepositorio)
    {
        this._poloRepositorio = poloRepositorio;
        _municipioRepositorio = municipioRepositorio;
        _dbContext = dbContext;
        _modelConverter = modelConverter;
        _escolaRepositorio = escolaRepositorio;
    }

    public async Task<Polo> ObterPorIdAsync(int id)
    {
        return await _poloRepositorio.ObterPorIdAsync(id);
    }
    
    
    public async Task CadastrarAsync(CadastroPoloDTO poloDto)
    {
        var municipioId = poloDto.MunicipioId;
        var municipio = await _municipioRepositorio.ObterPorIdAsync(municipioId);

        var poloNovo = _poloRepositorio.Criar(poloDto, municipio);

        var escolas = await _escolaRepositorio.ListarAsync();

        foreach (var escola in escolas)
        {
            var d = escola.CalcularDistanciaParaPolo(poloNovo);
            if (!d.HasValue) continue;
            var novoMaisPerto = d < escola.DistanciaPolo;
            escola.Polo = novoMaisPerto ? poloNovo : escola.Polo;
            escola.DistanciaPolo = novoMaisPerto ? d.Value : escola.DistanciaPolo;
        }
        await _dbContext.SaveChangesAsync();
    }

    public async Task<ListaPaginada<PoloModel>> ListarPaginadaAsync(PesquisaPoloFiltro filtro)
    {
        var polos = await _poloRepositorio.ListarPaginadaAsync(filtro);
        var poloModels = polos.Items.ConvertAll(_modelConverter.ToModel);
        return new ListaPaginada<PoloModel>(poloModels, polos.Pagina, polos.ItemsPorPagina, polos.Total);
    }
}
