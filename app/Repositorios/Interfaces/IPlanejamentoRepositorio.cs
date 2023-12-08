using api.Planejamento;
using app.Entidades;

namespace app.Repositorios.Interfaces
{
    public interface IPlanejamentoRepositorio
    {
        //Definir os metodos a serem implementados
        Task<PlanejamentoMacroDetalhadoModel> ObterPlanejamentoMacroDetalhado(Guid id);
        PlanejamentoMacro RegistrarPlanejamentoMacro(PlanejamentoMacro pm);
        Task<List<PlanejamentoMacro>> ListarAsync();
    }
}