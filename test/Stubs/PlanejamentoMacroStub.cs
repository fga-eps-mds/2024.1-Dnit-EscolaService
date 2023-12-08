using api;
using app.Entidades;
using System.Collections.Generic;
using System.Linq;


namespace test.Stubs
{
    public static class PlanejamentoMacroStub
    {
        public static IEnumerable<PlanejamentoMacro> ListarPlanejamentoMacro(IEnumerable<Municipio> municipios)
        {
            while (true)
            {
                var planejamentoMacro = new PlanejamentoMacro
                {
                    Id = Guid.NewGuid(),
                    Nome = $"Planejamento {Random.Shared.Next() % 10} - {Random.Shared.Next()}",
                    Responsavel = $"Joao - {Random.Shared.Next().ToString()}",
                    MesInicio = Mes.Abril,
                    MesFim = Mes.Julho,
                    AnoInicio = "2023",
                    AnoFim = "2023",
                    QuantidadeAcoes = Random.Shared.Next() % 10,
                };

                var planejamentoMacroEscola = ListarPlanejamentoMacroEscolas(municipios, planejamentoMacro).Take(Random.Shared.Next(0,5));
                planejamentoMacro.Escolas = planejamentoMacroEscola.ToList();

                yield return planejamentoMacro;
            }
        }

        public static IEnumerable<PlanejamentoMacroEscola> ListarPlanejamentoMacroEscolas(IEnumerable<Municipio> municipios, PlanejamentoMacro planejamentoMacro)
        {
            while (true)
            {
                var escola = EscolaStub.ListarEscolas(municipios, true).FirstOrDefault();

                var planejamentoMacroEscola = new PlanejamentoMacroEscola
                {
                    Id = Guid.NewGuid(),
                    Mes = planejamentoMacro.MesInicio,
                    Ano = planejamentoMacro.AnoInicio,
                    PlanejamentoMacroId = planejamentoMacro.Id,
                    PlanejamentoMacro = planejamentoMacro,
                    Escola = escola,
                    EscolaId = escola.Id
                };

                yield return planejamentoMacroEscola;
            }
        }
    }
}