using api.Escolas;
using app.Entidades;

namespace service.Interfaces
{
    public interface ICalcularRanqueJob
    {
        Task ExecutarAsync(int novoRanqueId, int timeoutMinutos);
        Task CalcularUpsEscolas(List<Escola> escolas);
        // public Task FinalizarCalcularRanqueJob(int ranqueId);
    }
}