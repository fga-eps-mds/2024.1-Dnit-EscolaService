using api.Escolas;

namespace service.Interfaces
{
    public interface ICalcularRanqueJob
    {
        Task ExecutarAsync(int novoRanqueId, int timeoutMinutos);
        // public Task FinalizarCalcularRanqueJob(int ranqueId);
    }
}