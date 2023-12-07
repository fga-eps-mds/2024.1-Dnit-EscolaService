using api.Planejamento;
using app.Entidades;

namespace service.Interfaces
{
    public interface IPlanejamentoService
    {
        // Definir os métodos a serem implementados
        Task<PlanejamentoMacroDetalhadoDTO> GerarRecomendacaoDePlanejamento(PlanejamentoMacroDTO planejamento);
        PlanejamentoMacro CriarPlanejamentoMacro(PlanejamentoMacroDetalhadoDTO planejamento);
        Task<PlanejamentoMacroDetalhadoModel> ObterPlanejamentoMacroDetalhado(Guid id);
        Task ExcluirPlanejamentoMacro(Guid id);
    }
}