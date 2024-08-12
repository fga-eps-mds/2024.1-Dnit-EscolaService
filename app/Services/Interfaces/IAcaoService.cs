using api;
using api.Acao.Request;
using api.Acao.Response;
using api.Atividades.Request;
using api.Atividades.Response;
using app.Entidades;
using Microsoft.AspNetCore.Mvc;

namespace service.Interfaces
{
    public interface IAcaoService
    {
        Task<ListaPaginada<AcaoPaginacaoResponse>> ListarPaginadaAsync(Guid escolaId,Guid planejamentoMacroEscolaId,PesquisaAcaoFiltro pesquisaAcaoFiltro);
        Task<ListaPaginada<AtividadePaginadaResponse>> ObterAtividadesAsync(Guid acaoId, PesquisaAtividadeFiltro pesquisaAtividadeFiltro);
    }
}

