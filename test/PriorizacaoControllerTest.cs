using app.Controllers;
using app.Entidades;
using System.Linq;
using System.Threading.Tasks;
using test.Fixtures;
using test.Stubs;
using Xunit.Abstractions;

namespace test
{
	public class PriorizacaoControllerTest : AuthTest
	{
        private readonly PriorizacaoController controller;
        private readonly AppDbContext db;

        public PriorizacaoControllerTest(ITestOutputHelper testOutputHelper, Base fixture) : base(testOutputHelper, fixture)
        {
            db = fixture.GetService<AppDbContext>(testOutputHelper)!;
            db.PopulaCustosLogisticos(4);
            controller = fixture.GetService<PriorizacaoController>(testOutputHelper)!;
        }

        [Fact]
        public async Task ListarCustosLogisticos_QuandoMetodoForChamado_RetornaCustosLogisticos()
        {
            var custosLogisticosDb = db.CustosLogisticos.ToList();
            var result = await controller.ListarCustosLogisticos();
            Assert.Equal(custosLogisticosDb.Count, result.Count);
        }

        [Fact]
        public async Task EditarCustosLogisticos_QuandoMetodoForChamado_RetornaParametrosDeCustoAtualizados()
        {
            var custoLogisticoAtualizado = CustoLogisticoStub.CustoLogisticoAtualizado();
            await controller.EditarCustosLogisticos(custoLogisticoAtualizado);
            var custoLogisticoDb = db.CustosLogisticos.ToList();
            Assert.Equal(custoLogisticoAtualizado[0].RaioMax, custoLogisticoDb[0].RaioMax);
            Assert.Equal(custoLogisticoAtualizado[0].Valor, custoLogisticoDb[0].Valor);
            Assert.Equal(custoLogisticoAtualizado[0].RaioMin, custoLogisticoDb[0].RaioMin);
            Assert.Equal(custoLogisticoAtualizado[0].Custo, custoLogisticoDb[0].Custo);
        }
    }
}