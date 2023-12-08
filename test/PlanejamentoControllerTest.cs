using System.Linq;
using api.Escolas;
using app.Controllers;
using app.Entidades;
using app.Services;
using System.Threading.Tasks;
using api.Polos;
using auth;
using Microsoft.EntityFrameworkCore;
using test.Fixtures;
using test.Stubs;
using Xunit.Abstractions;

namespace test
{
    public class PlanejamentoControllerTest : AuthTest, IDisposable
    {
        private readonly PlanejamentoController controller;
        private readonly AppDbContext dbContext;

        public PlanejamentoControllerTest(ITestOutputHelper testOutputHelper, Base fixture) : base(testOutputHelper, fixture)
        {
            dbContext = fixture.GetService<AppDbContext>(testOutputHelper)!;
            controller = fixture.GetService<PlanejamentoController>(testOutputHelper)!;
            AutenticarUsuario(controller);
            dbContext.PopulaPlanejamentoMacro(5);        
        }
        
        public new void Dispose()
        {
            dbContext.Clear();
        }
    }
}