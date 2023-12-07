using api.Planejamento;
using app.Entidades;
using app.Repositorios.Interfaces;

namespace app.Repositorios
{
    public class PlanejamentoRepositorio : IPlanejamentoRepositorio
    {
        private readonly AppDbContext dbContext;

        public PlanejamentoRepositorio(
            AppDbContext dbContext
        )
        {
            this.dbContext = dbContext;
        }

        // Implemetar os metodos da inteface

        public Task<List<PlanejamentoMacro>> ListarAsync()
        {
            throw new NotImplementedException();
        }

        Task<PlanejamentoMacroDetalhadoModel> IPlanejamentoRepositorio.ObterPlanejamentoMacroDetalhado(Guid id)
        {
            throw new NotImplementedException();
        }
    }
}