using api.Escolas;
using app.Entidades;

namespace app.Repositorios.Interfaces
{
    public interface ISolicitacaoAcaoRepositorio
    {
        public Task<SolicitacaoAcao> Criar(SolicitacaoAcaoData solicitacao, bool escolaJaCadastrada);
        public Task<SolicitacaoAcao?> ObterPorEscolaIdAsync(int escolaCodigoInep);
    }
}
