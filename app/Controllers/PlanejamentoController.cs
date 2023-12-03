using api;
using api.Planejamento;
using app.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace app.Controllers
{
    [ApiController]
    [Route("api/planejamento")]
    public class PlanejamentoController : AppController
    {
        //private readonly IPlanejamentoService planejamentoService;
        private readonly AuthService authService;

        public PlanejamentoController(
            //IPlanejamentoService planejamentoService,
            AuthService authService
        )
        {
            //this.planejamentoService = planejamentoService;
            this.authService = authService;
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> listarPlanejamentos(int pageIndex, int pageSize, string? nome = null)
        {
            authService.Require(Usuario, Permissao.PlanejamentoVisualizar);

            // Deve retornar uma lista paginada dos planejamentos cadastrados
            // ListaPaginada<PlanejamentoMacroModel>
            
            return Ok("Não implementado");
        }

        [HttpGet("{id}")]
        [Authorize]
        public async Task<IActionResult> ObterPlanejamentoMacro(Guid id)
        {
            authService.Require(Usuario, Permissao.PlanejamentoVisualizar);
            // Deve retornar um objeto PlanejamentoMacroDetalhado

            return Ok("Não implementado");
        }

        [HttpDelete("{id}")]
        [Authorize]
        public async Task<IActionResult> ExcluirPlanejamentoMacro(Guid id)
        {
            authService.Require(Usuario, Permissao.PlanejamentoRemover);
            // Deve retornar o status da operação

            return Ok("Não implementado");
        }

        [HttpPost]
        [Authorize]
        public IActionResult CriarPlanejamentoMacro([FromBody] PlanejamentoMacroDTO planejamentoMacro)
        {
            authService.Require(Usuario, Permissao.PlanejamentoCriar);
            // Deve retornar um objeto PlanejamentoMacroDetralhadoModel

            return Ok("Não implementado");
        }

        [HttpPut("{id}")]
        [Authorize]
        public async Task<IActionResult> EditarPlanejamentoMacro(Guid id, [FromBody] PlanejamentoMacroDetalhadoDTO planejamentoMacro)
        {
            authService.Require(Usuario, Permissao.PlanejamentoEditar);
            // Deve retornar um objeto PlanejamentoMacroDetalhadoModel

            return Ok("Não implementado");
        }
    }
}