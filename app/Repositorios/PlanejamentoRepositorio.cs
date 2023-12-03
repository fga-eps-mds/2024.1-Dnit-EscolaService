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

    }
}