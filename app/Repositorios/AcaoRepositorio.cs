using api;
using api.Acao.Request;
using app.Entidades;
using app.Repositorios.Interfaces;
using Microsoft.EntityFrameworkCore;

public class AcaoRepositorio : IAcaoRepositorio
{

    private readonly AppDbContext dbContext;

    public AcaoRepositorio(AppDbContext dbContext)
    {
        this.dbContext = dbContext;
    }

    public async Task<ListaPaginada<Acao>> ListarPaginadaAsync(Guid escolaId,Guid planejamentoMacroEscolaId,PesquisaAcaoFiltro filtro)
    {
        var query = dbContext.Acoes
            .Where(ac=>ac.EscolaId == escolaId)
            .Where(ac=>ac.PlanejamentoMacroEscolaId == planejamentoMacroEscolaId)
            .Include(e=>e.Atividade)
            .AsQueryable();

        if(filtro.Responsavel != null)
            query.Where(ac=>ac.GestorOperacional == filtro.Responsavel);
        
        if(filtro.Data != null)
            query.Where(ac=>ac.Data == filtro.Data);
        

        var total = await query.CountAsync();
        var items = await query
            .OrderBy(e => e.Data)
            .Skip((filtro.Pagina - 1) * filtro.TamanhoPagina)
            .Take(filtro.TamanhoPagina)
            .ToListAsync();

        return new ListaPaginada<Acao>(items, filtro.Pagina, filtro.TamanhoPagina, total);
    }
}
