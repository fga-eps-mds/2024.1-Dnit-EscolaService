using System.ComponentModel.DataAnnotations;
using api;

namespace app.Entidades
{
    
    public class Acao
    {
        [Key]
        public Guid Id{ get; set;}

        [Required]
        public Guid EscolasParticipantesPlanejamentoId { get; set;}

        public EscolasParticipantesPlanejamento EscolasParticipantesPlanejamento;

        public ICollection<Atividade> Atividade{get; set;}
    }
}