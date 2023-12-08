using app.Controllers;
using app.Entidades;
using app.Services;
using System.Threading.Tasks;
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
        
        [Fact]
        public async Task GetPlanejamento_QuandoExistir_DeveRetornar()
        {
            dbContext.PopulaPlanejamentoMacro(1);
            var primeiroPlanejamentoMacro = await dbContext.PlanejamentoMacro.FirstOrDefaultAsync();
            Assert.NotNull(primeiroPlanejamentoMacro);
            
            var planejamentoMacro = await controller.ObterPlanejamentoMacro(primeiroPlanejamentoMacro.Id);
            Assert.NotNull(planejamentoMacro);
            Assert.IsNotType<ApiException>(async () => await controller.ObterPlanejamentoMacro(primeiroPlanejamentoMacro.Id));
        }
        
        [Fact]
        public async Task GetPlanejamento_QuandoNaoExistir_DeveLancarExcessao()
        {
            dbContext.PopulaPlanejamentoMacro(1);
            await Assert.ThrowsAsync<ApiException>(async () => await controller.ObterPlanejamentoMacro(Guid.NewGuid()));
        }
        
        public new void Dispose()
        {
            dbContext.Clear();
        }
    }
}