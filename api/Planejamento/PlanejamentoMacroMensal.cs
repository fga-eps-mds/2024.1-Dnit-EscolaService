using api.Ranques;

namespace api.Planejamento
{
    public class PlanejamentoMacroMensalModel
    {
        public Mes Mes { get; set; }
        public string Ano { get; set; }
        public int UPSTotal { get; set; }
        public int QuantidadeEscolasTotal { get; set; }
        public int QuantidadeAlunosTotal { get; set; }
        public List<RanqueEscolaModel> Escolas { get; set; }
    }

    public class PlanejamentoMacroMensalDTO
    {
        public Mes Mes { get; set; }
        public string Ano { get; set; }
        public List <Guid> Escolas { get; set; }
    }
}