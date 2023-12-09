using app.Controllers;
using app.Entidades;
using app.Services;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
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
        public async Task ObterPlanejamentoMacro_QuandoExistir_DeveRetornar()
        {
            dbContext.PopulaPlanejamentoMacro(1);
            var planejBanco = await dbContext.PlanejamentoMacro.FirstOrDefaultAsync();
            Assert.NotNull(planejBanco);
            
            var planejamentoMacro = await controller.ObterPlanejamentoMacro(planejBanco.Id);
            Assert.NotNull(planejamentoMacro);
            Assert.IsNotType<ApiException>(async () => await controller.ObterPlanejamentoMacro(planejBanco.Id));
        }
        
        [Fact]
        public async Task ObterPlanejamentoMacro_QuandoNaoExistir_DeveLancarExcessao()
        {
            dbContext.PopulaPlanejamentoMacro(1);
            await Assert.ThrowsAsync<ApiException>(async () => await controller.ObterPlanejamentoMacro(Guid.NewGuid()));
        }
        
        [Fact]
        public async Task DeletePlanejamentoMacro_QuandoExistir_DeveDeletar()
        {
            dbContext.PopulaPlanejamentoMacro(1);
            var planejBanco = await dbContext.PlanejamentoMacro.FirstOrDefaultAsync();
            Assert.NotNull(planejBanco);
            
            await controller.ExcluirPlanejamentoMacro(planejBanco.Id);
            Assert.False(await dbContext.PlanejamentoMacro.AnyAsync(e => e.Id == planejBanco.Id));
            Assert.IsNotType<ApiException>(async () => await controller.ExcluirPlanejamentoMacro(planejBanco.Id));
        }
        
        [Fact]
        public async Task DeletePlanejamentoMacro_QuandoNaoExistir_DeveLancarExcecao()
        {
            dbContext.PopulaPlanejamentoMacro(1);
            await Assert.ThrowsAsync<ApiException>(async () => await controller.ExcluirPlanejamentoMacro(Guid.NewGuid()));
        }
        
        public new void Dispose()
        {
            dbContext.Clear();
        }
    }
}