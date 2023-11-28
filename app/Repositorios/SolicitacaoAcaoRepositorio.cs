using api.Escolas;
using app.Entidades;
using app.Repositorios.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace app.Repositorios
{
    public class SolicitacaoAcaoRepositorio : ISolicitacaoAcaoRepositorio
    {
        private readonly AppDbContext dbContext;

        public SolicitacaoAcaoRepositorio(AppDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<SolicitacaoAcao> Criar(SolicitacaoAcaoData s, bool escolaJaCadastrada)
        {
            var solicitacao = new SolicitacaoAcao
            {
                EscolaCodigoInep = s.EscolaCodigoInep,
                EscolaJaCadastrada = escolaJaCadastrada,
                Email = s.Email,
                Telefone = s.Telefone,
                NomeSolicitante = s.NomeSolicitante,
                DataRealizada = DateTimeOffset.Now,
                Observacoes = s.Observacoes,
            };
            await dbContext.Solicitacoes.AddAsync(solicitacao);
            return solicitacao;
        }

        public async Task<SolicitacaoAcao?> ObterPorEscolaIdAsync(int codigoInep)
        {
            return await dbContext.Solicitacoes
                .Where(e => e.EscolaCodigoInep == codigoInep)
                .FirstOrDefaultAsync();
        }
    }
}