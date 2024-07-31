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

        public Acao Acao{ get; set; }

        
    }
        
}