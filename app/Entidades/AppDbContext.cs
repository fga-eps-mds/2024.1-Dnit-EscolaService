using api;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualBasic.FileIO;

namespace app.Entidades
{
    public class AppDbContext : DbContext
    {
        public DbSet<Municipio> Municipios { get; set; }
        public DbSet<Escola> Escolas { get; set; }
        public DbSet<EscolaEtapaEnsino> EscolaEtapaEnsino { get; set; }
        public DbSet<Ranque> Ranques { get; set; }
        public DbSet<EscolaRanque> EscolaRanques { get; set; }
        public DbSet<Superintendencia> Superintendencias { get; set; }
        public DbSet<FatorPriorizacao> FatorPriorizacoes { get; set; }
        public DbSet<FatorCondicao> FatorCondicoes { get; set; }
        public DbSet<FatorEscola> FatorEscolas { get; set; }
        public DbSet<CustoLogistico> CustosLogisticos { get; set; }
        public DbSet<CondicaoValor> CondicaoValores { get; set; }
        public DbSet<FatorRanque> FatorRanques { get; set; }

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Escola>().HasMany(escola => escola.EtapasEnsino).WithOne(e => e.Escola);
            modelBuilder.Entity<Ranque>()
                .Property(r => r.Id).ValueGeneratedOnAdd();
            modelBuilder.Entity<EscolaRanque>()
                .Property(r => r.Id).ValueGeneratedOnAdd();

            modelBuilder.Entity<FatorEscola>()
                .HasKey(r => new { r.FatorPriorizacaoId, r.EscolaId });

            modelBuilder.Entity<FatorPriorizacao>()
                .HasMany<FatorCondicao>(fator => fator.FatorCondicoes);

            modelBuilder.Entity<CondicaoValor>()
                .Property(r => r.Id).ValueGeneratedOnAdd();

            modelBuilder.Entity<FatorCondicao>()
                .HasMany<CondicaoValor>(c => c.Valores);

            modelBuilder.Entity<FatorCondicao>()
                .Navigation(c => c.Valores)
                .AutoInclude();

            modelBuilder.Entity<FatorRanque>()
                .HasKey(r => new { r.FatorPriorizacaoId, r.RanqueId });

            modelBuilder.Entity<FatorRanque>()
                .HasOne(f => f.FatorPriorizacao);

            modelBuilder.Entity<FatorRanque>()
                .HasOne(f => f.Ranque);
        }

        public void Popula()
        {
            PopulaMunicipiosPorArquivo(null, Path.Join(".", "Migrations", "Data", "municipios.csv"));

            PopulaSuperintendenciasPorArquivo(null, Path.Join(".", "Migrations", "Data", "superintendencias.csv"));

            PopulaCustosLogisticosPorArquivo(null, Path.Join(".", "Migrations", "Data", "custoslogisticos.csv"));
        }

        public void PopulaCustosLogisticosPorArquivo(int? limit, string caminho)
        {
            var hasCustosLogisticos = CustosLogisticos.Any();
            var custosLogisticos = new List<CustoLogistico>();

            if (hasCustosLogisticos)
            {
                return;
            }

            using (var fs = File.OpenRead(caminho))
            using (var parser = new TextFieldParser(fs))
            {
                parser.TextFieldType = FieldType.Delimited;
                parser.SetDelimiters(",");

                var columns = new Dictionary<string, int> { { "custo", 0 }, { "raioMin", 1 }, 
                    { "raioMax", 2 }, { "valor", 3 } };

                while (!parser.EndOfData)
                {
                    var row = parser.ReadFields()!;
                    var custoLogistico = new CustoLogistico
                    {
                        Custo = int.Parse(row[columns["custo"]]),
                        RaioMin = int.Parse(row[columns["raioMin"]]),
                        Valor = int.Parse(row[columns["valor"]])
                    };

                    if (!string.IsNullOrEmpty(row[columns["raioMax"]]))
                    {
                        custoLogistico.RaioMax = int.Parse(row[columns["raioMax"]]);
                    }

                    custosLogisticos.Add(custoLogistico);
                    if (limit.HasValue && custosLogisticos.Count >= limit.Value)
                    {
                        break;
                    }
                }
            }

            AddRange(custosLogisticos);
            SaveChanges();
        }

        public List<Municipio>? PopulaMunicipiosPorArquivo(int? limit, string caminho)
        {
            var hasMunicipio = Municipios.Any();
            var municipios = new List<Municipio>();

            if (hasMunicipio)
            {
                return null;
            }

            using (var fs = File.OpenRead(caminho))
            using (var parser = new TextFieldParser(fs))
            {
                parser.TextFieldType = FieldType.Delimited;
                parser.SetDelimiters(",");

                var columns = new Dictionary<string, int> { { "id", 0 }, { "name", 1 }, { "uf", 2 } };

                while (!parser.EndOfData)
                {
                    var row = parser.ReadFields()!;
                    var municipio = new Municipio
                    {
                        Id = int.Parse(row[columns["id"]]),
                        Nome = row[columns["name"]],
                        Uf = (UF)int.Parse(row[columns["uf"]]),
                    };

                    municipios.Add(municipio);
                    if (limit.HasValue && municipios.Count >= limit.Value)
                    {
                        break;
                    }
                }
            }

            AddRange(municipios);
            SaveChanges();
            return municipios;
        }

        public List<Superintendencia>? PopulaSuperintendenciasPorArquivo(int? limit, string caminho)
        {
            var hasSuperintendencias = Superintendencias.Any();
            var superintendencias = new List<Superintendencia>();

            if (hasSuperintendencias)
                return null;

            using (var fs = File.OpenRead(caminho))
            using (var parser = new TextFieldParser(fs))
            {
                parser.TextFieldType = FieldType.Delimited;
                parser.SetDelimiters(";");

                var columns = new Dictionary<string, int>
                {
                    { "id", 0 }, { "endereco", 1 }, { "cep", 2 }, { "latitude", 3 }, { "longitude" , 4}, { "uf" , 5}
                };

                while (!parser.EndOfData)
                {
                    var row = parser.ReadFields()!;
                    var superintendencia = new Superintendencia
                    {
                        Id = int.Parse(row[columns["id"]]),
                        Endereco = row[columns["endereco"]],
                        Cep = row[columns["cep"]],
                        Latitude = row[columns["latitude"]],
                        Longitude = row[columns["longitude"]],
                        Uf = (UF)int.Parse(row[columns["uf"]]),
                    };

                    superintendencias.Add(superintendencia);
                    limit--;

                    if (limit == 0)
                        break;
                }
            }

            AddRange(superintendencias);
            SaveChanges();
            return superintendencias;

        }

    }
}