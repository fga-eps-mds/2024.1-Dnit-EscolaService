using api;
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

        public async Task<SolicitacaoAcao> Criar(SolicitacaoAcaoData s, bool escolaJaCadastrada, Escola? escola)
        {
            var solicitacao = new SolicitacaoAcao
            {
                EscolaCodigoInep = s.EscolaCodigoInep,
                Email = s.Email,
                Telefone = s.Telefone,
                NomeSolicitante = s.NomeSolicitante,
                DataRealizada = DateTimeOffset.Now,
                Observacoes = s.Observacoes,
                EscolaJaCadastrada = escola != null,
                EscolaId = escola?.Id,
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

        public async Task<ListaPaginada<SolicitacaoAcao>> ObterSolicitacoesAsync(PesquisaSolicitacaoFiltro filtro)
        {
            // FIXME: tem que funcionar apenas com a opção de qtd MÍNIMA de alunos (ex: acima de 1001 alunos)
            // if (filtro.QuantidadeAlunosMin != null)
            // {
            //     query = query.Where(e => e.TotalAlunos >= filtro.QuantidadeAlunosMin);
            //     if (filtro.QuantidadeAlunosMax != null)
            //         query = query.Where(e => e.TotalAlunos <= filtro.QuantidadeAlunosMax);
            // }

            var query = dbContext.Solicitacoes.Include(s => s.Escola).AsQueryable();

            var total = await query.CountAsync();
            var sols = await query.ToListAsync();
            return new ListaPaginada<SolicitacaoAcao>(sols, filtro.Pagina, filtro.TamanhoPagina, total);
        }
    }
}