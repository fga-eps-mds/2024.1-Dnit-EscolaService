using System.Threading.Tasks;
using app.Entidades;
using app.Repositorios.Interfaces;
using app.Services;
using Microsoft.EntityFrameworkCore;
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
        
        [Fact]
        public async Task ObterPlanejamentoMacroPorIdAsync_QuandoExistir_DeveRetornar()
        {
            dbContext.PopulaPlanejamentoMacro(1);
            var planejBanco = await dbContext.PlanejamentoMacro.FirstOrDefaultAsync();
            Assert.NotNull(planejBanco);
            
            var planejamentoMacro = await planejamentoRepositorio.ObterPlanejamentoMacroAsync(planejBanco.Id);
            Assert.Equal(planejBanco, planejamentoMacro);
            Assert.IsNotType<ApiException>(async () => await planejamentoRepositorio.ObterPlanejamentoMacroAsync(planejBanco.Id));
        }
        
        [Fact]
        public async Task ObterPlanejamentoMacroPorIdAsync_QuandoNaoExistir_DeveLancarExcecao()
        {
            await Assert.ThrowsAsync<ApiException>(() => planejamentoRepositorio.ObterPlanejamentoMacroAsync(Guid.NewGuid()));
        }
        
        [Fact]
        public async Task DeletePlanejamentoMacro_QuandoExistir_DeveDeletar()
        {
            dbContext.PopulaPlanejamentoMacro(1);
            var planejamento = await dbContext.PlanejamentoMacro.FirstOrDefaultAsync();
            Assert.NotNull(planejamento);

            planejamentoRepositorio.ExcluirPlanejamentoMacro(planejamento);
            await dbContext.SaveChangesAsync();

            Assert.False(await dbContext.PlanejamentoMacro.AnyAsync(e => e.Id == planejamento.Id));
            Assert.IsNotType<ApiException>(() => planejamentoRepositorio.ExcluirPlanejamentoMacro(planejamento));
        }

        [Fact]
        public async Task DeletePlanejamentoMacro_QuandoNaoExistir_DeveLancarExcecao()
        {
            await Assert.ThrowsAsync<ApiException>(async() => await planejamentoRepositorio.ObterPlanejamentoMacroAsync(Guid.NewGuid()));
        }

        public new void Dispose()
        {
            dbContext.Clear();
        }
    }
}

