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

        public async Task<SolicitacaoAcao> Criar(SolicitacaoAcaoData s, Escola? escolaCadastrada)
        {
            var solicitacao = new SolicitacaoAcao
            {
                EscolaCodigoInep = s.EscolaCodigoInep,
                Email = s.Email,
                Telefone = s.Telefone,
                NomeSolicitante = s.NomeSolicitante,
                DataRealizada = DateTimeOffset.Now,
                Observacoes = s.Observacoes,
                EscolaId = escolaCadastrada?.Id,
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
            var query = dbContext.Solicitacoes
                .Include(s => s.Escola!)
                    .ThenInclude(e => e.Municipio)
                .Include(s => s.EscolaMunicipio)
                .AsQueryable();

            // if (filtro.Nome != null)
            //     query = query.Where(s=> s.Escola)
            
            // FIXME: tem que funcionar apenas com a opção de qtd MÍNIMA de alunos (ex: acima de 1001 alunos)
            // if (filtro.QuantidadeAlunosMin != null)
            // {
            //     query = query.Where(e => e.Escola.TotalAlunos >= filtro.QuantidadeAlunosMin);
            //     if (filtro.QuantidadeAlunosMax != null)
            //         query = query.Where(e => e.TotalAlunos <= filtro.QuantidadeAlunosMax);
            // }

            var total = await query.CountAsync();
            var sols = await query
                .OrderByDescending(s => s.DataRealizadaUtc)
                .Skip((filtro.Pagina - 1) * filtro.TamanhoPagina)
                .Take(filtro.TamanhoPagina)
                .ToListAsync();
            return new ListaPaginada<SolicitacaoAcao>(sols, filtro.Pagina, filtro.TamanhoPagina, total);
        }
    }
}