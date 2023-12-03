
using app.Entidades;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;

namespace test.Stubs
{
    public static class AppDbContextExtensions
    {
        private static List<Municipio>? municipios;
        private static Mutex municipios_mutex = new Mutex();

        public static List<Escola> PopulaEscolas(this AppDbContext dbContext, int limite = 1, bool comEtapas = true)
        {
            dbContext.Clear();
            var escolas = new List<Escola>();
            
            if (!dbContext.Municipios.Any())
            {
                dbContext.PopulaMunicipios(limite);
            }
            
            var municipios = dbContext.Municipios.Take(1).ToList();

            foreach (var escola in EscolaStub.ListarEscolas(municipios, comEtapas).Take(limite))
            {
                dbContext.Add(escola);
                escolas.Add(escola);
            }
            dbContext.SaveChanges();
            return escolas;
        }

        public static List<Municipio> PopulaMunicipios(this AppDbContext dbContext, int limit)
        {
            dbContext.Clear();
            if (municipios != default && limit <= municipios.Count)
            {
                var resultado = municipios.Take(limit).ToList();
                dbContext.AddRange(resultado);
                dbContext.SaveChanges();
                return resultado;
            }
            municipios_mutex.WaitOne(int.MaxValue);
            var caminho = Path.Join("..", "..", "..", "Stubs", "municipios.csv");
            var municipiosDb = dbContext.PopulaMunicipiosPorArquivo(limit, caminho)!;
            municipios = municipiosDb.ToList();
            municipios_mutex.ReleaseMutex();
            return municipiosDb;
        }

        public static List<Polo> PopulaPolos(this AppDbContext dbContext, int limit, int idStart = 1)
        {
            if (!dbContext.Municipios.Any())
            {
                dbContext.PopulaMunicipios(limit);
            }

            var listaMunicipios = dbContext.Municipios.Take(1).ToList();
            var polos = PoloStub.Listar(listaMunicipios, idStart).Take(limit).ToList();
            dbContext.AddRange(polos);
            dbContext.SaveChanges();
            return polos;
        }

        public static void Clear(this AppDbContext dbContext)
        {
            dbContext.RemoveRange(dbContext.Escolas);
            dbContext.RemoveRange(dbContext.EscolaEtapaEnsino);
            dbContext.RemoveRange(dbContext.Municipios);
            dbContext.RemoveRange(dbContext.EscolaRanques);
            dbContext.RemoveRange(dbContext.Ranques);
            dbContext.RemoveRange(dbContext.Polos);
            dbContext.SaveChanges();
        }
    }
}
