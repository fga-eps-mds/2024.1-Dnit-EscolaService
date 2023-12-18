using app.Entidades;

namespace service.Interfaces
{
    public interface ICalcularRanqueJob
    {
        Task ExecutarAsync(int novoRanqueId, bool calcularUps);
        Task CalcularUpsEscolas(List<Escola> escolas);
        bool ExisteFatorEscola(FatorPriorizacao fator, Escola escola);
        public bool ExisteEscolaRanque(Escola escola, int ranqueId);
    }
}