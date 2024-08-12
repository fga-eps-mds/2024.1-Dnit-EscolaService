namespace api.Atividades.Response
{
    public class AtividadePaginadaResponse
    {
        public Guid IdAtividade { get; set; }
        public Guid IdAcao { get; set; }
        public TimeSpan Horario { get; set; }
        public string NomeResponsavel { get; set; }
        public string Local { get; set; }
        public List<string> Monitores { get; set; }        
        public string Turma { get; set; }
    }
}

