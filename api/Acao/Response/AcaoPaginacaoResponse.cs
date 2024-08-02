namespace api.Acao.Response
{
    public class AcaoPaginacaoResponse
    {
        public Guid IdAcao { get; set; }
        public DateTime Data { get; set; }
        public string GestorOperacional { get; set; }
        public SituacaoAcao SituacaoAprovacao { get; set; }        
        public SituacaoAcao SituacaoVisita { get; set; }
    }
}

