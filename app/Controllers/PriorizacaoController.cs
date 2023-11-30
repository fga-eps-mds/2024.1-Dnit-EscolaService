using api.CustoLogistico;
using app.Services;
using app.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace app.Controllers
{
    [ApiController]
    [Route("api/priorizacao")]
    public class PriorizacaoController: AppController
    {
        private readonly IPriorizacaoService priorizacaoService;
        private readonly AuthService authService;
        public PriorizacaoController(
            IPriorizacaoService priorizacaoService, AuthService authService
        )
        {
            this.priorizacaoService = priorizacaoService;
            this.authService = authService;
        }

        [HttpGet("custologistico")]
        public async Task<List<CustoLogisticoItem>> ListarCustosLogisticos()
        {
            return await priorizacaoService.ListarCustosLogisticos();
        }
    }
}
