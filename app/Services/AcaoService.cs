using api;
using api.Acao.Request;
using api.Acao.Response;
using app.Repositorios.Interfaces;
using app.Services;
using service.Interfaces;

public class AcaoService : IAcaoService
{
    public readonly IAcaoRepositorio acaoRepositorio;
    public readonly ModelConverter modelConverter;
    public AcaoService(IAcaoRepositorio acaoRepositorio,ModelConverter modelConverter)
    {
        this.acaoRepositorio=acaoRepositorio;
        this.modelConverter=modelConverter;
    }

    public AcaoService(IAcaoRepositorio acaoRepositorio)
    {
        this.acaoRepositorio=acaoRepositorio;
    }
    public async Task<ListaPaginada<AcaoPaginacaoResponse>> ListarPaginadaAsync(Guid escolaId,Guid planejamentoMacroEscolaId,PesquisaAcaoFiltro pesquisaAcaoFiltro)
    {
        var listaPaginadaAcoes = await acaoRepositorio.ListarPaginadaAsync(escolaId,planejamentoMacroEscolaId,pesquisaAcaoFiltro);
        var listaAcoesResponse = listaPaginadaAcoes.Items.ConvertAll(modelConverter.ToModel);
        return new ListaPaginada<AcaoPaginacaoResponse>(listaAcoesResponse,listaPaginadaAcoes.Pagina,listaPaginadaAcoes.ItemsPorPagina, listaPaginadaAcoes.Total);
    }
}



