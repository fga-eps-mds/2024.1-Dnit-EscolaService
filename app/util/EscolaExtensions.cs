using System.Globalization;
using app.Entidades;

namespace app.util;

public static class EscolaExtensions
{
    public static double? CalcularDistanciaParaPolo(this Escola escola, Polo polo)
    {

        bool escolaVazia = String.IsNullOrEmpty(escola.Latitude) || String.IsNullOrEmpty(escola.Longitude);
        bool poloVazio = String.IsNullOrEmpty(polo.Latitude) || String.IsNullOrEmpty(polo.Longitude);

        if (escolaVazia || poloVazio)
            return null;
        
        CultureInfo c = new CultureInfo("pt-BR"); 
        (double elat, double elon) = (double.Parse(escola.Latitude), double.Parse(escola.Longitude));
        (double plat, double plon) = (double.Parse(polo.Latitude), double.Parse(polo.Longitude));

        return GeoCalc.CalcularDistancia(elat, elon, plat, plon);
    }
}
