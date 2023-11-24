using api.Escolas;
using app.Controllers;
using app.Entidades;
using app.Services;
using System.Threading.Tasks;
using test.Fixtures;
using test.Stubs;
using Xunit.Abstractions;

namespace test
{
    public class PolosControllerTest : AuthTest, IDisposable
    {
        private readonly PolosController controller;
        private readonly AppDbContext dbContext;

        public PolosControllerTest(ITestOutputHelper testOutputHelper, Base fixture) : base(testOutputHelper, fixture)
        {
            dbContext = fixture.GetService<AppDbContext>(testOutputHelper)!;
            controller = fixture.GetService<PolosController>(testOutputHelper)!;
            AutenticarUsuario(controller);
            dbContext.PopulaPolos(5);
        }

        [Fact]
        public async Task GetPolo_QuandoExistir_DeveRetornar()
        {
            var polo = await controller.Obter(1);

            Assert.NotNull(polo);
            Assert.Equal(1, polo.Id);
            Assert.NotNull(polo.Cep);
            Assert.NotNull(polo.Longitude);
            Assert.NotNull(polo.Latitude);
            Assert.NotNull(polo.Endereco);
        }

        [Fact]
        public async Task GetPolo_QuandoNaoExistir_DeveLancarExcessao()
        {
            await Assert.ThrowsAsync<ApiException>(async () => await controller.Obter(0));
        }

        public new void Dispose()
        {
            dbContext.Clear();
        }
    }
}
