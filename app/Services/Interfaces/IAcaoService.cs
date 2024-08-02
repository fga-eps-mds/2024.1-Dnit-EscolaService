using api;
using api.Acao.Request;
using api.Acao.Response;
using app.Entidades;
using Microsoft.AspNetCore.Mvc;

namespace service.Interfaces
{
    public interface IAcaoService
    {
        Task<ListaPaginada<AcaoPaginacaoResponse>> ListarPaginadaAsync(Guid escolaId,PesquisaAcaoFiltro pesquisaAcaoFiltro);
    }
}

