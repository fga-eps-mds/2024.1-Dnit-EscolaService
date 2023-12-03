using System.Linq;
using System.Threading.Tasks;
using api.Polos;
using app.Entidades;
using app.Repositorios.Interfaces;
using app.Services;
using app.Services.Interfaces;
using test.Fixtures;
using test.Stubs;
using Xunit.Abstractions;
using Xunit.Microsoft.DependencyInjection.Abstracts;

namespace test;

public class PoloServiceTest : TestBed<Base>, IDisposable
{
    private readonly IPoloService poloService;
    private readonly IPoloRepositorio poloRepositorio;
    private readonly AppDbContext dbContext;
    private readonly ModelConverter modelConverter;
    public PoloServiceTest(ITestOutputHelper testOutputHelper, Base fixture) : base(testOutputHelper, fixture)
    {
        dbContext = fixture.GetService<AppDbContext>(testOutputHelper)!;
        poloService = fixture.GetService<IPoloService>(testOutputHelper)!;
        modelConverter = fixture.GetService<ModelConverter>(testOutputHelper)!;
        poloRepositorio = fixture.GetService<IPoloRepositorio>(testOutputHelper)!;
        dbContext.PopulaMunicipios(5);
        dbContext.PopulaPolos(5);
    }

    [Fact]
    public async Task ObterPorIdAsync_QuandoExistir_DeveRetornar()
    {
        var polo = await poloService.ObterPorIdAsync(1);
        
        Assert.NotNull(polo);
        Assert.Equal(1, polo.Id);
        Assert.NotNull(polo.Nome);
        Assert.NotNull(polo.Municipio);
        Assert.NotNull(polo.Cep);
        Assert.NotNull(polo.Longitude);
        Assert.NotNull(polo.Latitude);
        Assert.NotNull(polo.Endereco);
    }

    [Fact]
    public async Task ObterPorIdAsync_QuandoNaoExistir_DeveLancarExcecao()
    {
        await Assert.ThrowsAsync<ApiException>(() => poloService.ObterPorIdAsync(-9999));
    }

    [Fact]
    public async Task ObterModelPorIdAsync_QuandoExistir_DeveRetornar()
    {
        var polo = await poloService.ObterModelPorIdAsync(1);
        
        Assert.NotNull(polo);
        Assert.Equal(1, polo.Id);
        Assert.NotNull(polo.Nome);
        Assert.NotNull(polo.Municipio);
        Assert.NotNull(polo.Cep);
        Assert.NotNull(polo.Longitude);
        Assert.NotNull(polo.Latitude);
        Assert.NotNull(polo.Endereco);
    }

    [Fact]
    public async Task ObterModelPorIdAsync_QuandoNaoExistir_DeveLancarExcecao()
    {
        await Assert.ThrowsAsync<ApiException>(() => poloService.ObterModelPorIdAsync(-9999));
    }

    [Fact]
    public async void ListarPaginadaAsync_QuandoFiltroForPassado_DeveRetornarListaDePolosFiltrados()
    {
        var poloPesquisado = dbContext.Polos.First();
        var poloModeloPesquisado = modelConverter.ToModel(poloPesquisado);
        var filtro = new PesquisaPoloFiltro()
        {
            Pagina = 1,
            TamanhoPagina = 2,
            Nome = poloPesquisado.Nome,
            IdUf = (int?) poloPesquisado.Uf,
            Cep = poloPesquisado.Cep,
            IdMunicipio = poloPesquisado.MunicipioId,
        };

        var listaPaginada = await poloService.ListarPaginadaAsync(filtro);
        
        // Referências diferentes
        var poloResposta = listaPaginada.Items[0];
        Assert.Equal(poloModeloPesquisado.Nome, poloModeloPesquisado.Nome);
        Assert.Equal(poloModeloPesquisado.Uf?.Id, poloResposta.Uf?.Id);
        Assert.Equal(poloModeloPesquisado.Cep, poloResposta.Cep);
        Assert.Equal(poloModeloPesquisado.Municipio?.Id, poloResposta.Municipio?.Id);
    }
    
    [Fact]
    public async Task ListarPaginadaAsync_QuandoMetodoForChamado_DeveRetornarListaDePolos()
    {
        var polos = dbContext.Polos.ToList();
        var filtro = new PesquisaPoloFiltro {
            Pagina = 1,
            TamanhoPagina = polos.Count()
        };
        var result = await poloService.ListarPaginadaAsync(filtro);

        Assert.Equal(polos.Count(), result.Total);
        Assert.Equal(filtro.Pagina, result.Pagina);
        Assert.Equal(filtro.TamanhoPagina, result.ItemsPorPagina);
        Assert.True(polos.All(e => result.Items.Exists(ee => ee.Id == e.Id)));
    }
    
    [Fact]
    public async Task ListarPaginadaAsync_QuandoFiltroNaoExistir_DeveRetornarListaVazia()
    {
        var polos = dbContext.Polos.ToList();
        var filtro = new PesquisaPoloFiltro {
            Pagina = 999999,
            TamanhoPagina = 9999999
        };
        var result = await poloService.ListarPaginadaAsync(filtro);
        
        Assert.Empty(result.Items);
    }

    [Fact]
    public async Task CadastrarAsync_QuandoChamadoDeveCadastrarPoloEmBanco()
    {
        var cadastro = new CadastroPoloDTO()
        {
            Cep = "1",
            Endereco = "avenida",
            Id = -1,
            Latitude = "1,2",
            Longitude = "1,2",
            Nome = "polo",
            MunicipioId = 5200050,
            IdUf = 1,
        };

        await poloService.CadastrarAsync(cadastro);

        var meuPolo = dbContext.Polos.Where(p => p.Id == -1).First();

        Assert.NotNull(meuPolo);
    }
    
    public new void Dispose()
    {
        dbContext.Clear();
    }
}
