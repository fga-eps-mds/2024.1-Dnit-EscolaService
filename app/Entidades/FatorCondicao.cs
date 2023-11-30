using System.ComponentModel.DataAnnotations;

namespace app.Entidades
{
    public class FatorCondicao
    {

        [Key]
        public Guid Id { get; set; }

        [Required, MaxLength(30)]
        public string Propriedade { get; set; }

        [Required]
        public int Operador { get; set; }

        [Required, MaxLength(250)]
        public string Valor { get; set; }

        [Required]
        public Guid FatorPriorizacaoId { get; set; }

        public FatorPriorizacao FatorPriorizacao { get; set; }

    }
}
