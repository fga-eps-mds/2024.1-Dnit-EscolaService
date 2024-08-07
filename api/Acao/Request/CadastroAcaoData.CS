using api;

namespace api.Acao.Request
{
    public class CadastroAcaoData
    {
        public Guid PlanejamentoMacroEscolaId { get; set; }
        public DateTime Data { get; set; }
        public string GestorOperacional { get; set; }
        public Guid EscolaId { get; set; }
        public string NomeEscola { get; set; }
        public TurnoEscola TurnoEscola { get; set; }
        public HorarioDisponivel HorarioDisponivel { get; set; }
        public int QuantidadeAlunos { get; set; }
        public int QuantidadeTurmas { get; set; }
        public NivelEscolar NivelEscolar { get; set; }

        public string EspacoDisponivel { get; set; }
        public string TamanhoDoEspaco { get; set; }
        public bool EspacoCoberto { get; set; }
        public bool WiFi { get; set; }
        
    }

    public class HorarioDisponivel
    {
        public TimeSpan  De { get; set; }
        public TimeSpan  Ate { get; set; }
        
    }
}