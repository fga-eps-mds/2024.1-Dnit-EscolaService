using api;
using api.Escolas;

namespace test.Stubs
{
    public class SolicitacaoAcaoStub
    {
        public SolicitacaoAcaoData ObterSolicitacaoAcaoDTO()
        {
            return new()
            {
                Escola = "Escola Teste",
                Uf = UF.DF,
                Municipio = "Brasília",
                NomeSolicitante = "João Testador",
                VinculoEscola = "Professor",
                Email = "joao@email.com",
                Telefone = "123123123",
                CiclosEnsino = new[] { "Ensino Médio", "Ensino Fundamental" },
                QuantidadeAlunos = 503,
                Observacoes = "Teste de Solicitação"
            };
        }
    }
}
