using api;
using api.CustoLogistico;
using api.Fatores;
using app.Services;
using app.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
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

        [Authorize]
        [HttpGet("fatores")]
        public async Task<List<FatorPrioriModel>> ListarFatores()
        {
            authService.Require(Usuario, Permissao.PrioridadesVisualizar);
            return await priorizacaoService.ListarFatores();
        }

        [Authorize]
        [HttpGet("fatores/{Id}")]
        public async Task<FatorPrioriModel> VisualizarFatorId([FromRoute] Guid Id)
        {
            authService.Require(Usuario, Permissao.PrioridadesVisualizar);
            return await priorizacaoService.VisualizarFatorId(Id);
        }

        [Authorize]
        [HttpPost("fatores")]
        public async Task<FatorPrioriModel> AdicionarFatorPriorizacao([FromBody] FatorPrioriModel novoFator)
        {
            authService.Require(Usuario, Permissao.PrioridadesEditar);
            return await priorizacaoService.AdicionarFatorPriorizacao(novoFator);
        }

        [Authorize]
        [HttpGet("custologistico")]
        public async Task<List<CustoLogisticoItem>> ListarCustosLogisticos()
        {
            authService.Require(Usuario, Permissao.PrioridadesVisualizar);
            return await priorizacaoService.ListarCustosLogisticos();
        }

        [Authorize]
        [HttpPut("custologistico")]
        public async Task<IActionResult> EditarCustosLogisticos([FromBody] List<CustoLogisticoItem> items)
        {
            authService.Require(Usuario, Permissao.PrioridadesEditar);
            try
            {
                var listaAtualizada = await priorizacaoService.EditarCustosLogisticos(items);
                return Ok(listaAtualizada);
            }
            catch (InvalidOperationException ex)
            {
                return StatusCode(400, ex.Message);
            }
        }

        [Authorize]
        [HttpDelete("fatores/{Id}")]
        public async Task<IActionResult> DeletarFator(Guid Id)
        {
            authService.Require(Usuario, Permissao.PrioridadesExcluir);
            try
            {
                await priorizacaoService.DeletarFatorId(Id);
                return Ok("Fator Excluido");
            }
            catch(InvalidOperationException e)
            {
                return StatusCode(400, e.Message);
            }
        }

        [Authorize]
        [HttpPut("fatores/{Id}")]
        public async Task<FatorPrioriModel> EditarFator(Guid Id, [FromBody] FatorPrioriModel fatorPrioriModel)
        {
            authService.Require(Usuario, Permissao.PrioridadesEditar);
            fatorPrioriModel.Id = Id;
            return await priorizacaoService.EditarFatorPorId(Id, fatorPrioriModel);
        }
    }
}
