using api.CustoLogistico;
using api.Fatores;
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

        [HttpGet("fatores")]
        public async Task<List<FatorPrioriModel>> ListarFatores()
        {
            return await priorizacaoService.ListarFatores();
        }

        [HttpGet("fatores/{Id}")]
        public async Task<FatorPrioriModel> VisualizarFatorId([FromRoute] Guid Id)
        {
            return await priorizacaoService.VisualizarFatorId(Id);
        }

        [HttpGet("custologistico")]
        public async Task<List<CustoLogisticoItem>> ListarCustosLogisticos()
        {
            return await priorizacaoService.ListarCustosLogisticos();
        }

        [HttpPut("custologistico")]
        public async Task<IActionResult> EditarCustosLogisticos([FromBody] List<CustoLogisticoItem> items)
        {
            try
            {
                var listaAtualizada = await priorizacaoService.EditarCustosLogisticos(items);
                return Ok(listaAtualizada);
            }
            catch (InvalidOperationException ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
    }
}
