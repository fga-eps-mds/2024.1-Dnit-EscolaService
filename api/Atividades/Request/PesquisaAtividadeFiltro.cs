namespace api.Atividades.Request
{
    public class PesquisaAtividadeFiltro
    {
        public int Pagina { get; set; } = 1;
        public int TamanhoPagina { get; set; } = 10;
    }
}
