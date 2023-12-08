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
        // Task<List<PlanejamentoMacro>> ListarAsync();
    }
}