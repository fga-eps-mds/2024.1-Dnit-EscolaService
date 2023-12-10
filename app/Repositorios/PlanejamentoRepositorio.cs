using api.Planejamento;
using app.Entidades;
using app.Repositorios.Interfaces;
using app.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query.SqlExpressions;

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

        public void ExcluirPlanejamentoMacro(PlanejamentoMacro pm)
        {
            dbContext.PlanejamentoMacro.Remove(pm);
        }

        // Implemetar os metodos da inteface

        public void ExcluirPlanejamentoMacroEscola(PlanejamentoMacroEscola pm)
        {
            dbContext.PlanejamentoMacroEscola.Remove(pm);
        }

        // public async Task<List<PlanejamentoMacro>> ListarAsync()
        // {
        //     throw new NotImplementedException();
        // }

        public async Task<PlanejamentoMacro> ObterPlanejamentoMacroAsync(Guid id)
        {
            return await dbContext.PlanejamentoMacro
                .Include(p => p.Escolas)
                .ThenInclude(e => e.Escola)
                .FirstOrDefaultAsync(x => x.Id==id) ?? throw new ApiException(api.ErrorCodes.PlanejamentoMacroNaoEncontrado);
        }

        public  PlanejamentoMacro RegistrarPlanejamentoMacro(PlanejamentoMacro pm)
        {
            dbContext.PlanejamentoMacro.Add(pm);
            return pm;
        }

        public void RegistrarPlanejamentoMacroMensal(PlanejamentoMacroEscola pme)
        {
           dbContext.PlanejamentoMacroEscola.Add(pme);
        }
    }
}