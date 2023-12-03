using System.Threading.Tasks;
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
    
    public PoloServiceTest(ITestOutputHelper testOutputHelper, Base fixture) : base(testOutputHelper, fixture)
    {
        dbContext = fixture.GetService<AppDbContext>(testOutputHelper)!;
        poloService = fixture.GetService<IPoloService>(testOutputHelper)!;
        poloRepositorio = fixture.GetService<IPoloRepositorio>(testOutputHelper)!;
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

    public new void Dispose()
    {
        dbContext.Clear();
    }
}
