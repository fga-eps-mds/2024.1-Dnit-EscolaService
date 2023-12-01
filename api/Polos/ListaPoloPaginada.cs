namespace api.Polos;

public class ListaPoloPaginada<T>
{
    public int Pagina { get; set; }
    public int PolosPorPagnia { get; set; }
    public int TotalPolos { get; set; }
    public int TotalPaginas { get; set; }
    public List<T> Polos { get; set; }


    public ListaPoloPaginada(IEnumerable<T> polos, int paginaIndex, int polosPorPagnia, int totalPolos)
    {
        Pagina = paginaIndex;
        PolosPorPagnia = polosPorPagnia;
        TotalPolos = totalPolos;
        TotalPaginas = (int)Math.Ceiling(TotalPolos / (double)PolosPorPagnia);
        Polos = new List<T>(polos);
    }
}
