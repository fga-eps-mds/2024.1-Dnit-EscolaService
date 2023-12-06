using app.Entidades;

namespace service.Interfaces
{
    public interface IPlanejamentoService
    {
        // Definir os métodos a serem implementados
        Task<PlanejamentoMacro> ObterPlanejamentoMacro(Guid id);
    }
}