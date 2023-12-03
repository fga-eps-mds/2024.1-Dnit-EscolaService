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
        var municipios = await poloRepositorio.ListarAsync();
        Assert.Empty(municipios);
    }
}
