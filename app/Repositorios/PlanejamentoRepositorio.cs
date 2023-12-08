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
        
        public  PlanejamentoMacro RegistrarPlanejamentoMacro(PlanejamentoMacro pm)
        {
            // var planejamentoMacro = new PlanejamentoMacro
            // {
            //     Nome = pm.Nome,
            //     Responsavel = pm.Responsavel,
            //     MesInicio = pm.MesInicio,
            //     AnoInicio = pm.AnoInicio,
            //     MesFim = pm.MesFim,
            //     AnoFim = pm.AnoFim,
            //     QuantidadeAcoes = pm.QuantidadeAcoes
            // };
            // List<PlanejamentoMacroEscola> pmEscolas = new List<PlanejamentoMacroEscola>();
            // foreach (var escola in escolas)
            // {
            //     var escolaAtual = new PlanejamentoMacroEscola
            //     {
            //         Mes = pm.MesInicio,
            //         Ano = pm.AnoInicio,
            //         PlanejamentoMacroId = planejamentoMacro.Id,
            //         PlanejamentoMacro = planejamentoMacro,
            //         EscolaId = escola.Id,
            //         Escola = escola
            //     };
            //     pmEscolas.Add(escolaAtual);
            // }

            // planejamentoMacro.Escolas = pmEscolas;
            // dbContext.Add(planejamentoMacro);
            // return planejamentoMacro;
            dbContext.PlanejamentoMacro.Add(pm);
            return pm;
        }

        Task<PlanejamentoMacroDetalhadoModel> IPlanejamentoRepositorio.ObterPlanejamentoMacroDetalhado(Guid id)
        {
            throw new NotImplementedException();
        }
    }
}