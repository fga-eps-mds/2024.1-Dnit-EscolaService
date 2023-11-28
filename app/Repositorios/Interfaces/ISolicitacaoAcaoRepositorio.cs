using api;
using api.Escolas;
using app.Entidades;

namespace app.Repositorios.Interfaces
{
    public interface ISolicitacaoAcaoRepositorio
    {
        public Task<SolicitacaoAcao> Criar(SolicitacaoAcaoData solicitacao, bool escolaJaCadastrada, Escola? escola);
        public Task<ListaPaginada<SolicitacaoAcao>> ObterSolicitacoesAsync(PesquisaSolicitacaoFiltro filtro);
        public Task<SolicitacaoAcao?> ObterPorEscolaIdAsync(int escolaCodigoInep);
    }
}
