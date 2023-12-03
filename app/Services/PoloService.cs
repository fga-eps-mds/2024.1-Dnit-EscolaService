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
    private readonly IEscolaRepositorio _escolaRepositorio;
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
    
    public async Task<PoloModel> ObterModelPorIdAsync(int id)
    {
        var polo = await _poloRepositorio.ObterPorIdAsync(id);
        return _modelConverter.ToModel(polo);
    }
    
    public async Task CadastrarAsync(CadastroPoloDTO poloDto)
    {
        var municipioId = poloDto.MunicipioId;
        var municipio = await _municipioRepositorio.ObterPorIdAsync(municipioId);

        var poloNovo = _poloRepositorio.Criar(poloDto, municipio);

        var escolas = await _escolaRepositorio.ListarAsync();
        escolas.ForEach(e => e.SubstituirSeMaisProximo(poloNovo));

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

        var escolas = await _escolaRepositorio.ListarAsync();
        escolas.ForEach(e => e.SubstituirSeMaisProximo(poloExistente));

        await _dbContext.SaveChangesAsync();
    }

    public async Task ExcluirAsync(Polo poloExistente)
    {

        var escolas = _escolaRepositorio.ListarAsync();
        foreach (var escola in escolas)
        {
            if (escola.Polo != null && escola.Polo.Id == poloExistente.Id)
            {
                List<Polo> polos; 
                await _poloRepositorio.ListarAsync().ContinueWith(l => l);
                Polo novoPoloMaisProximo = escola.Polo;
                double menorDistancia = escola.DistanciaPolo;

                foreach (var polo in polosRestantes)
                {
                    var distancia = escola.CalcularDistanciaParaPolo(polo);
                    if (distancia.HasValue && distancia.Value < menorDistancia)
                    {
                        novoPoloMaisProximo = polo;
                        menorDistancia = distancia.Value;
                    }
                }

                escola.Polo = novoPoloMaisProximo;
                escola.DistanciaPolo = menorDistancia;
            }
        }
        _poloRepositorio.Excluir(poloExistente);

        await _dbContext.SaveChangesAsync();
    }
}
