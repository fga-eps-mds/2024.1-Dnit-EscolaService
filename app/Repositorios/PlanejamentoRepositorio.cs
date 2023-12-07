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

        public PlanejamentoMacro RegistrarPlanejamentoMacro(PlanejamentoMacroDTO p)
        {
            var planejamento = new PlanejamentoMacro
            {   
                Nome=p.Nome,
                Responsavel=p.Responsavel,
                MesInicio=p.MesInicio,
                MesFim=p.MesFim,
                AnoInicio=p.AnoInicio,
                AnoFim=p.AnoFim,
                QuantidadeAcoes=p.QuantidadeAcoes
            };
            dbContext.Add(planejamento);
            return planejamento;
        }

        Task<PlanejamentoMacroDetalhadoModel> IPlanejamentoRepositorio.ObterPlanejamentoMacroDetalhado(Guid id)
        {
            throw new NotImplementedException();
        }
    }
}