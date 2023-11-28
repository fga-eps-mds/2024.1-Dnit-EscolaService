using api;
using api.Escolas;
using api.Solicitacoes;
using app.Entidades;
using app.Services;
using Microsoft.AspNetCore.Mvc;
using service.Interfaces;
using System.Net.Mail;

namespace app.Controllers
{
    [ApiController]
    [Route("api/solicitacaoAcao")]
    public class SolicitacaoAcaoController : AppController
    {
        private readonly ISolicitacaoAcaoService solicitacaoAcaoService;

        public SolicitacaoAcaoController(ISolicitacaoAcaoService solicitacaoAcaoService)
        {
            this.solicitacaoAcaoService = solicitacaoAcaoService;
        }

        [HttpPost]
        public async Task<IActionResult> EnviarSolicitacaoAcao([FromBody] SolicitacaoAcaoData solicitacaoAcaoDTO)
        {
            try
            {
                solicitacaoAcaoService.EnviarSolicitacaoAcao(solicitacaoAcaoDTO);
                await solicitacaoAcaoService.Criar(solicitacaoAcaoDTO);
                return Ok();
            }
            catch (SmtpException)
            {
                return StatusCode(500, "Falha no envio do email.");
            }
        }

        [HttpGet]
        public async Task<ListaPaginada<SolicitacaoAcaoModel>> ObterSolicitacoesAsync([FromQuery] PesquisaSolicitacaoFiltro filtro)
        {
            return await solicitacaoAcaoService.ObterSolicitacoesAsync(filtro);
        }

        [HttpGet("escolas")]
        public async Task<IEnumerable<EscolaInep>> ObterEscolas([FromQuery] int municipio)
        {
            var escolas = await solicitacaoAcaoService.ObterEscolas(municipio);
            return escolas;
        }
    }
}
