using api;
using api.Escolas;
using app.Entidades;

namespace app.Repositorios.Interfaces
{
    public interface IEscolaRepositorio
    {
        Task<Escola> ObterPorIdAsync(Guid id, bool incluirEtapas = false, bool incluirMunicipio = false);
        Task<List<Escola>> ListarAsync();
        Escola Criar(CadastroEscolaData escolaData, Municipio municipio, double distanciaSuperintendencia = 0, Polo? superintendencia = null);
        Escola Criar(EscolaModel escola, double distanciaSuperintendencia = 0, Polo? superintendencia = null);
        Task<ListaPaginada<Escola>> ListarPaginadaAsync(PesquisaEscolaFiltro filtro);
        Task<Escola?> ObterPorCodigoAsync(int codigo, bool incluirEtapas = false, bool incluirMunicipio = false);
        EscolaEtapaEnsino AdicionarEtapaEnsino(Escola escola, EtapaEnsino etapa);
    }
}
