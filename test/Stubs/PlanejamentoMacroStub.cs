using api;
using app.Entidades;
using System.Collections.Generic;
using System.Linq;


namespace test.Stubs
{
    public static class PlanejamentoMacroStub
    {

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