using api;
using api.Planejamento;
using app.Entidades;
using app.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Migrations;
using service.Interfaces;

namespace app.Controllers
{
    [ApiController]
    [Route("api/planejamento")]
    public class PlanejamentoController : AppController
    {
        private readonly IPlanejamentoService planejamentoService;
        private readonly AuthService authService;
        private readonly ModelConverter modelConverter;

        public PlanejamentoController(
            IPlanejamentoService planejamentoService,
            AuthService authService,
            ModelConverter modelConverter
        )
        {
            this.planejamentoService = planejamentoService;
            this.authService = authService;
            this.modelConverter = modelConverter;
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
            return Ok(await planejamentoService.ObterPlanejamentoMacroDetalhado(id));
        }

        [HttpDelete("{id}")]
        [Authorize]
        public async Task ExcluirPlanejamentoMacro(Guid id)
        {
            authService.Require(Usuario, Permissao.PlanejamentoRemover);
            // Deve retornar o status da operação
            await planejamentoService.ExcluirPlanejamentoMacro(id);
            //return Ok("Não implementado");
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> CriarPlanejamentoMacro([FromBody] PlanejamentoMacroDTO planejamentoMacro)
        {
            authService.Require(Usuario, Permissao.PlanejamentoCriar);
            var recomendacao = await planejamentoService.GerarRecomendacaoDePlanejamento(planejamentoMacro); //retorna entidade sem id
            var planejamentoMacroCriado = planejamentoService.CriarPlanejamentoMacro(recomendacao);
            var planejamentoMacroDetalhadoModel = modelConverter.ToModel(planejamentoMacroCriado);
            
            //inserir no banco

            //pegar objeto do banco e retornar como model para o front (usar o ModelConverter)

            
            // deve registrar o planejamento macro no banco

            return Ok(planejamentoMacroDetalhadoModel);
            // Deve retornar um objeto PlanejamentoMacroDetralhadoModel
            // está retornando essa recomendação só para testar pelo swagger o retorno 
            // da geração  de recomendação 
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