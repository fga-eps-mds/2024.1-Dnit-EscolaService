using System.Collections.Generic;
using api.Fatores;
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

        public static FatorPrioriModel ObterPriorizacaoComCondicao()
        {
            return new FatorPrioriModel
            {
                Id = Guid.NewGuid(),
                Nome = "TesteModel",
                Peso = 12,
                Ativo = true,
                Primario = false,
                FatorCondicoes = new List<FatorCondicaoModel>{
                    new FatorCondicaoModel
                    {
                        Id = Guid.NewGuid(),
                        Propriedade = "PropriedadeModel",
                        Operador = 1,
                        Valor = "1",
                    }
                }
            };
        }

        public static FatorCondicao ObterCondicao()
        {
            return new FatorCondicao
                    {
                        Id = Guid.NewGuid(),
                        Propriedade = "PropriedadeModel1",
                        Operador = 12,
                        Valor = "11",
                    };
          
        }
        public static IEnumerable<FatorPrioriModel> ListarFatorPrioriModel(bool hasId = true)
        {
            while (true)
            {
                var fatorPriorizacao = new FatorPrioriModel
                {
                    Nome = $"Fator {Random.Shared.Next()}",
                    Ativo = true,
                    Peso = Random.Shared.Next() % 100,
                    Primario = true,
                    FatorCondicoes = new List<FatorCondicaoModel>(),
                };

                if (hasId == true)
                {
                    fatorPriorizacao.Id = Guid.NewGuid();
                }

                yield return fatorPriorizacao;
            }

        }
    }
}