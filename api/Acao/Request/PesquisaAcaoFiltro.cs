namespace api.Acao.Request
{
    public class PesquisaAcaoFiltro
    {
        public int Pagina { get; set; } = 1;
        public int TamanhoPagina { get; set; } = 10;
        public string? Responsavel { get; set; }
        public DateTime? Data { get; set; }
    }
}
