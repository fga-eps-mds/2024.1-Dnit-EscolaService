using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using app.Entidades;
using app.Repositorios.Interfaces;
using app.Services.Interfaces;
using test.Fixtures;
using test.Stubs;
using Xunit.Abstractions;
using Xunit.Microsoft.DependencyInjection.Abstracts;

namespace test;

public class PoloRepositorioTest: TestBed<Base>, IDisposable
{
    private readonly IPoloRepositorio poloRepositorio;
    private readonly AppDbContext dbContext;
    
    public PoloRepositorioTest(ITestOutputHelper testOutputHelper, Base fixture) : base(testOutputHelper, fixture)
    {
        dbContext = fixture.GetService<AppDbContext>(testOutputHelper)!;
        poloRepositorio = fixture.GetService<IPoloRepositorio>(testOutputHelper)!;
    }
    
    [Fact]
    public async Task ListarAsync_QuandoVazio_DeveRetornarListaVazia()
    {
        var polos = await poloRepositorio.ListarAsync();
        Assert.Empty(polos);
    }

    
    [Fact]
    public async Task ListarAsync_QuandoPreenchido_DeveRetornarListaCompleta()
    {
        var polosDb = dbContext.PopulaPolos(5)!;
        var polos = await poloRepositorio.ListarAsync(null);
        Assert.Equal(polosDb.Count, polos.Count);
        Assert.True(polosDb.All(mdb => polos.Exists(m => m.Id == mdb.Id)));
    }

    [Fact]
    public async Task ListarAsync_QuandoFiltro_DeveRetornarListaFiltrada()
    {
        var polosDb = dbContext.PopulaPolos(5);
        var filtroUF = polosDb.First().Uf;
        var polos = await poloRepositorio.ListarAsync(p => p.Uf == filtroUF);
        Assert.Equal(polosDb.Where(p => p.Uf == filtroUF).Count(), polos.Count);
    }

    [Fact]
    public async Task ListarAsync_QuandoNaoExistir_DeveRetornarListaVazia()
    {
        var polosDb = dbContext.PopulaPolos(5);
        Expression<Func<Polo, bool>> filtroUf =  p => p.Id == -99;

        var Polos = await poloRepositorio.ListarAsync(filtroUf);
        Assert.Empty(Polos);
    }

    public new void Dispose()
    {
        dbContext.Clear();
    }
}
