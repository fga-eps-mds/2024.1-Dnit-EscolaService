using app.Controllers;
using app.Entidades;
using app.Repositorios.Interfaces;
using test.Fixtures;
using Xunit.Abstractions;
using Xunit.Microsoft.DependencyInjection.Abstracts;

namespace test
{
	public class PriorizacaoRepositorioTest : TestBed<Base>, IDisposable
    {
		IPriorizacaoRepositorio priorizacaoRepositorio;
		AppDbContext db;

		public PriorizacaoRepositorioTest(ITestOutputHelper testOutputHelper, Base fixture) : base(testOutputHelper, fixture)
        {
			priorizacaoRepositorio = fixture.GetService<IPriorizacaoRepositorio>(testOutputHelper)!; ;
			db = fixture.GetService<AppDbContext>(testOutputHelper)!;
		}
	}
}