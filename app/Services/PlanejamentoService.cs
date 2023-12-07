using System.Numerics;
using api.Planejamento;
using app.Entidades;
using app.Repositorios.Interfaces;
using service.Interfaces;

namespace app.Services
{
    public class PlanejamentoService : IPlanejamentoService
    {
        private readonly IPlanejamentoRepositorio planejamentoRepositorio;
        private readonly ModelConverter modelConverter;
        private readonly AppDbContext dbContext;

        public PlanejamentoService
        (
            IPlanejamentoRepositorio planejamentoRepositorio,
            ModelConverter modelConverter,
            AppDbContext dbContext
        )
        {
            this.planejamentoRepositorio = planejamentoRepositorio;
            this.modelConverter = modelConverter;
            this.dbContext = dbContext;
        }

        // Implementar os metodos da inteface
        public async Task<PlanejamentoMacroDetalhadoModel> ObterPlanejamentoMacroDetalhado(Guid id)
        {
            return await planejamentoRepositorio.ObterPlanejamentoMacroDetalhado(id);
        }

        public async Task ExcluirPlanejamentoMacro(Guid id)
        {
            var planejamento = await planejamentoRepositorio.ObterPlanejamentoMacroDetalhado(id);
            dbContext.Remove(planejamento);
            await dbContext.SaveChangesAsync();
        }

        public Task<PlanejamentoMacroDetalhadoDTO> GerarRecomendacaoDePlanejamento(PlanejamentoMacroDTO planejamento)
        {
            throw new NotImplementedException();
        }

        public PlanejamentoMacro CriarPlanejamentoMacro(PlanejamentoMacroDetalhadoDTO planejamento)
        {
            
            throw new NotImplementedException();
        }
    }
}