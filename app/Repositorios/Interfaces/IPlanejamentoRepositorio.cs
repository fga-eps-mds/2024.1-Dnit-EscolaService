using app.Entidades;

namespace app.Repositorios.Interfaces
{
    public interface IPlanejamentoRepositorio
    {
        //Definir os metodos a serem implementados
        Task<PlanejamentoMacro> ObterPlanejamentoMacro(Guid id);
        Task<List<PlanejamentoMacro>> ListarAsync();
    }
}