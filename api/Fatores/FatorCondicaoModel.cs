using System.ComponentModel.DataAnnotations;
using api.Fatores;

namespace api.Fatores
{
    public class FatorCondicaoModel
    {
        public Guid Id { get; set; }
        public int Propriedade { get; set; }
        public int Operador { get; set; }
        public List<string> Valores { get; set; }
        public Guid? FatorPriorizacaoId { get; set; }
    }
}
