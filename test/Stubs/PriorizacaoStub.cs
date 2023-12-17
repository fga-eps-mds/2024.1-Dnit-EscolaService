using System.Collections.Generic;
using api;
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
                    Nome = "UPS",
                    Peso = 80,
                    Ativo = true,
                    Primario = false,
                    DeleteTime = null
                },

                new FatorPriorizacao{
                    Id = Guid.NewGuid(),
                    Nome = "Custo Logistico",
                    Peso = 50,
                    Ativo = true,
                    Primario = false,
                    DeleteTime = null
                },
                new FatorPriorizacao{
                    Id = Guid.NewGuid(),
                    Nome = "teste1",
                    Peso = 13,
                    Ativo = false,
                    Primario = false,
                    DeleteTime = DateTime.UtcNow
                },
                new FatorPriorizacao{
                    Id = Guid.NewGuid(),
                    Nome = "teste2",
                    Peso = 14,
                    Ativo = true,
                    Primario = false,
                    DeleteTime = null
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
                        Propriedade = PropriedadeCondicao.Situacao,
                        Operador = OperacaoCondicao.Equals,
                        Valores = new List<string>{"1"},
                    }
                }
            };
        }

        public static FatorCondicao ObterCondicao()
        {
            Guid id = Guid.NewGuid();
            return new FatorCondicao
                    {
                        Id = id,
                        Propriedade = PropriedadeCondicao.Municipio,
                        Operador = OperacaoCondicao.Equals,
                        Valores = new List<CondicaoValor>{
                            new CondicaoValor
                            {
                                FatorCondicaoId = id,
                                Valor = "2"
                            }
                        },
                    };
          
        }

        public static FatorEscola ObterFatorEscola()
        {
            return new FatorEscola{
                FatorPriorizacaoId = Guid.NewGuid(),
                FatorPriorizacao = new FatorPriorizacao{
                    Nome = "nome",
                },
                EscolaId = Guid.NewGuid(),
                Escola = new Escola{
                    Nome = "nome",
                    Codigo = 123,
                    Endereco = "Rua1",
                    Latitude = "1124",
                    Longitude = "1234",
                    Cep = "12345662",
                    Telefone = "12345123"
                },
                Valor = 12,
            };
        }

        public static FatorRanque ObterFatorRanque()
        {
            return new FatorRanque {
                FatorPriorizacaoId = Guid.NewGuid(),
                RanqueId = 1
            };
        }

        public static EscolaRanque ObterEscolaRanque(Guid escolaId, int ranqueId)
        {
            return new EscolaRanque {
                Id = Random.Shared.Next(),
                EscolaId = escolaId,
                RanqueId = ranqueId,
                Pontuacao = Random.Shared.Next(),
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