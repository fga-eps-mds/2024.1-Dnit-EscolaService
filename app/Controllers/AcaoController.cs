using api;
using api.Acao.Request;
using api.Acao.Response;
using api.Atividades.Request;
using api.Atividades.Response;
using app.Repositorios.Interfaces;
using app.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using service.Interfaces;

namespace app.Controllers
{
    [ApiController]
    [Route("api/acoes")]
    public class AcaoController : AppController
    {
        public IAcaoService acaoService { get; set; }
        public AcaoController(IAcaoService acaoService)
        {
            this.acaoService = acaoService;
        }


        [Authorize]
        [HttpGet("{escolaId:guid}/{planejamentoMacroEscolaId:guid}/acao")]
        public async Task<ListaPaginada<AcaoPaginacaoResponse>> ObterAcoesAsync([FromRoute] Guid escolaId, [FromRoute] Guid planejamentoMacroEscolaId, [FromQuery] PesquisaAcaoFiltro pesquisaAcaoFiltro)
        {
            //TODO : Todos os Perfis podem visualizar uma ação?
            // authService.Require(Usuario, Permissao.EscolaVisualizar);

            return await acaoService.ListarPaginadaAsync(escolaId,planejamentoMacroEscolaId,pesquisaAcaoFiltro);
        }

        [Authorize]
        [HttpGet("{acaoId:guid}/atividades")]
        public async Task<ListaPaginada<AtividadePaginadaResponse>> ObterAtividadesAsync([FromRoute] Guid acaoId, [FromQuery] PesquisaAtividadeFiltro pesquisaAtividadeFiltro)
        {
            return await acaoService.ObterAtividadesAsync(acaoId, pesquisaAtividadeFiltro);
        }

        
    }
}