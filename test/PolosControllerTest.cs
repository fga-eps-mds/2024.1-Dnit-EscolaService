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
            Assert.NotNull(polo.Nome);
            Assert.NotNull(polo.Municipio);
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
        
        [Fact]
        public async Task ObterPolosAsync_QuandoMetodoForChamado_DeveRetornarListaDePolos()
        {
            var polos = dbContext.Polos.ToList();
            var filtro = new PesquisaPoloFiltro {
                Pagina = 1,
                TamanhoPagina = polos.Count()
            };
            var result = await controller.ObterPolosAsync(filtro);

            Assert.Equal(polos.Count(), result.Total);
            Assert.Equal(filtro.Pagina, result.Pagina);
            Assert.Equal(filtro.TamanhoPagina, result.ItemsPorPagina);
            Assert.True(polos.All(e => result.Items.Exists(ee => ee.Id == e.Id)));
        }

        [Fact]
        public async Task ObterPolosAsync_QuandoNaoTiverPermissao_DeveSerBloqueado()
        {
            var polos = dbContext.Polos.ToList();
            var filtro = new PesquisaPoloFiltro()
            {
                Pagina = 1,
                TamanhoPagina = polos.Count(),
            };
            AutenticarUsuario(controller, permissoes: new() { });

            await Assert.ThrowsAsync<AuthForbiddenException>(async () => await controller.ObterPolosAsync(filtro));
        }

        public new void Dispose()
        {
            dbContext.Clear();
        }
    }
}
