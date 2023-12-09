
using app.Entidades;
using app.Repositorios.Interfaces;
using test.Fixtures;
using test.Stubs;
using Xunit.Abstractions;
using Xunit.Microsoft.DependencyInjection.Abstracts;

namespace test
{
    public class PlanejamentoServiceTest : TestBed<Base>, IDisposable
    {
        private readonly IPlanejamentoRepositorio planejamentoRepositorio;
        private readonly AppDbContext dbContext;
        
        public PlanejamentoServiceTest(ITestOutputHelper testOutputHelper, Base fixture) : base(testOutputHelper, fixture)
        {
            dbContext = fixture.GetService<AppDbContext>(testOutputHelper)!;
            planejamentoRepositorio = fixture.GetService<IPlanejamentoRepositorio>(testOutputHelper)!;
            dbContext.PopulaPlanejamentoMacro(5);
        }
        
        
        public new void Dispose()
        {
            dbContext.Clear();
        }
    }
}

