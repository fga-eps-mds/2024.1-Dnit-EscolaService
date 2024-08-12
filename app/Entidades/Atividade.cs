using System.ComponentModel.DataAnnotations;
using api;

namespace app.Entidades
{
    
    public class Atividade
    {
        [Key]
        public Guid Id{ get; set;}

        [Required]
        public Guid AcaoId{ get; set;}

        public TimeSpan Horario { get; set; }

        public string NomeResponsavel { get; set; }

        public string Local { get; set; }

        public List<string> Monitores { get; set; } 

        public string Turma { get; set; }

        public Acao Acao{ get; set; }       
    }
        
}