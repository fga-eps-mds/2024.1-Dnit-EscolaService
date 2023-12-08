using System.Collections.Generic;
using app.Entidades;

namespace test.Stubs
{
    public class PriorizacaoStub
    {
        public static List<FatorPriorizacao> ObterListaPriorizacoes()
        {
            return new List<FatorPriorizacao>{
                
                new FatorPriorizacao{
                    Id = Guid.NewGuid(),
                    Nome = "teste",
                    Peso = 1,
                    Ativo = true,
                    Primario = false
                },

                new FatorPriorizacao{
                    Id = Guid.NewGuid(),
                    Nome = "teste1",
                    Peso = 12,
                    Ativo = true,
                    Primario = false
                },
                new FatorPriorizacao{
                    Id = Guid.NewGuid(),
                    Nome = "teste2",
                    Peso = 13,
                    Ativo = true,
                    Primario = false
                },
                new FatorPriorizacao{
                    Id = Guid.NewGuid(),
                    Nome = "teste3",
                    Peso = 14,
                    Ativo = true,
                    Primario = false
                },
                
            };
        }
    }
}