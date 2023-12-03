using System.Numerics;
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
    }
}