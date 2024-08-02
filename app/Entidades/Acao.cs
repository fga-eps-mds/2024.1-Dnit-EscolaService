using System.ComponentModel.DataAnnotations;
using api;

namespace app.Entidades
{
    
    public class Acao
    {
        [Key]
        public Guid Id{ get; set;}
        
        [Required]
        public DateTime Data { get; set; }

        [Required]
        public string GestorOperacional { get; set; }
        
        [Required]
        public SituacaoAcao SituacaoAprovacao { get; set; }
        
        [Required]
        public SituacaoAcao SituacaoVisita { get; set; }
        
        [Required]
        public Guid EscolaId { get; set; }
        
        [Required]
        public Guid PlanejamentoMacroEscolaId { get; set; }

        public EscolasParticipantesPlanejamento EscolasParticipantesPlanejamento{get;set;}  

        public ICollection<Atividade> Atividade{get; set;}
    }
}