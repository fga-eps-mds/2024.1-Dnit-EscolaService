using System.Linq.Expressions;
using api;
using api.Planejamento;
using app.Entidades;

namespace app.Repositorios.Interfaces
{
    public interface IPlanejamentoRepositorio
    {
        //Definir os metodos a serem implementados
        void ExcluirPlanejamentoMacro(PlanejamentoMacro pm);
        void ExcluirPlanejamentoMacroEscola(PlanejamentoMacroEscola pm);
        Task<PlanejamentoMacro> ObterPlanejamentoMacroAsync(Guid id);
        PlanejamentoMacro RegistrarPlanejamentoMacro(PlanejamentoMacro pm);
<<<<<<< HEAD
        Task<ListaPaginada<PlanejamentoMacro>> ListarPaginadaAsync(PesquisaPlanejamentoFiltro filtro);
        Task<List<PlanejamentoMacro>> ListarAsync(Expression<Func<PlanejamentoMacro, bool>>? filter = null);
=======
        // Task<List<PlanejamentoMacro>> ListarAsync();

        void RegistrarPlanejamentoMacroMensal(PlanejamentoMacroEscola pme);
>>>>>>> 0adb0228ee92301afb010d0af9e4973560fa44a7
    }
}