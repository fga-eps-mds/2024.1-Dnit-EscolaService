using api.CustoLogistico;
using app.Entidades;
using app.Repositorios.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace app.Repositorios
{
    public class PriorizacaoRepositorio : IPriorizacaoRepositorio
    {
        private readonly AppDbContext dbContext;

        public PriorizacaoRepositorio(
            AppDbContext dbContext
        )
        {
            this.dbContext = dbContext;
        }

        public async Task<List<CustoLogistico>> ListarCustosLogisticosAsync()
        {
            return await dbContext.CustosLogisticos.ToListAsync();
        }
    }
}
