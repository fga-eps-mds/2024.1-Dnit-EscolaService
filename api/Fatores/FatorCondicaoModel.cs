using System.ComponentModel.DataAnnotations;
using api.Fatores;

namespace api.Fatores
{
    public class FatorCondicaoModel
    {
        public Guid Id { get; set; }
        public string Propriedade { get; set; }
        public int Operador { get; set; }
        public string Valor { get; set; }
        public Guid FatorPriorizacaoId { get; set; }
        public FatorPrioriModel FatorPriorizacao { get; set; }

    }
}
