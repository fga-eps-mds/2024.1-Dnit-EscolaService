using app.Entidades;
using app.Repositorios.Interfaces;
using app.Services.Interfaces;
using service.Interfaces;
using test.Fixtures;
using Xunit.Abstractions;
using Xunit.Microsoft.DependencyInjection.Abstracts;

namespace test
{
	public class PriorizacaoServiceTest : TestBed<Base>
	{
		private readonly IPriorizacaoService priorizacaoService;
		private readonly IPriorizacaoRepositorio priorizacaoRepositorio;
		private readonly AppDbContext db;

		public PriorizacaoServiceTest(ITestOutputHelper testOutputHelper, Base fixture) : base(testOutputHelper, fixture)
        {
			priorizacaoService = fixture.GetService<IPriorizacaoService>(testOutputHelper)!;
			priorizacaoRepositorio = fixture.GetService<IPriorizacaoRepositorio>(testOutputHelper)!;
            db = fixture.GetService<AppDbContext>(testOutputHelper)!;
        }
	}
}