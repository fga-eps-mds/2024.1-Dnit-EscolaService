using System.Linq.Expressions;
using api;
using api.Acao.Request;
using api.Escolas;
using app.Entidades;

namespace app.Repositorios.Interfaces
{
    public interface IAcaoRepositorio
    {
        Task<ListaPaginada<Acao>> ListarPaginadaAsync(Guid escolaId,Guid planejamentoMacroEscolaId,PesquisaAcaoFiltro filtro);
    }
}
