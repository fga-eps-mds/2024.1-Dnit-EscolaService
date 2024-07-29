using System.ComponentModel.DataAnnotations;
using api;

namespace app.Entidades
{
    public class EscolasParticipantesPlanejamento
    {
        [Key]
        public Guid Id { get; set; }
        
        [Required]
        public Guid EscolaId { get; set; }

        public Escola Escola { get; set; }

        [Required]
        public Guid PlanejamentoMacroEscolaId  { get; set; }

        public PlanejamentoMacroEscola PlanejamentoMacroEscola { get; set; }
    }
}