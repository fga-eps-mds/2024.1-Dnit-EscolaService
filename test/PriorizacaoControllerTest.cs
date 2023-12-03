using app.Controllers;
using app.Entidades;
using test.Fixtures;
using Xunit.Abstractions;

namespace test
{
	public class PriorizacaoControllerTest : AuthTest
	{
        private readonly PriorizacaoController controller;
        private readonly AppDbContext db;

        public PriorizacaoControllerTest(ITestOutputHelper testOutputHelper, Base fixture) : base(testOutputHelper, fixture)
        {
            controller = fixture.GetService<PriorizacaoController>(testOutputHelper)!; ;
            db = fixture.GetService<AppDbContext>(testOutputHelper)!; ;
        }   
    }
}