using Moq;
using service.Interfaces;
using Microsoft.Extensions.Configuration;
using System.Net.Http;
using System.Linq;
using System.Threading.Tasks;
using test.Stubs;
using app.Services;
using app.Repositorios.Interfaces;
using app.Entidades;
using Xunit.Microsoft.DependencyInjection.Abstracts;
using test.Fixtures;
using Xunit.Abstractions;

namespace test
{
    public class SolicitacaoAcaoServicePersistenciaTest : TestBed<Base>
    {
        private readonly Mock<ISmtpClientWrapper> smtpClientWrapperMock;
        private readonly Mock<IHttpClientFactory> httpClientFactoryMock;
        private readonly Mock<IConfiguration> configurationMock;
        private readonly ISolicitacaoAcaoRepositorio solicitacaoAcaoRepositorio;
        private readonly IEscolaRepositorio escolaRepositorio;
        private readonly AppDbContext db;
        private readonly SolicitacaoAcaoService service;

        public SolicitacaoAcaoServicePersistenciaTest(ITestOutputHelper testOutputHelper, Base fixture) : base(testOutputHelper, fixture)
        {
            // Environment.SetEnvironmentVariable("EMAIL_SERVICE_ADDRESS", "teste_email@exemplo.com");
            // Environment.SetEnvironmentVariable("EMAIL_SERVICE_PASSWORD", "teste");
            // Environment.SetEnvironmentVariable("EMAIL_DNIT", "teste_email@exemplo.com");

            db = fixture.GetService<AppDbContext>(testOutputHelper)!;
            db.Clear();
            smtpClientWrapperMock = new();
            httpClientFactoryMock = new();
            configurationMock = new();

            solicitacaoAcaoRepositorio = fixture.GetService<ISolicitacaoAcaoRepositorio>(testOutputHelper)!;
            escolaRepositorio = fixture.GetService<IEscolaRepositorio>(testOutputHelper)!;

            service = new SolicitacaoAcaoService(db, smtpClientWrapperMock.Object, httpClientFactoryMock.Object, configurationMock.Object, solicitacaoAcaoRepositorio, escolaRepositorio);
        }

        [Fact]
        public async Task Criar_QuandoEscolaSemSolicitacao_DeveCriarSolicitacao()
        {
            Assert.Empty(db.Solicitacoes);

            var sol = new SolicitacaoAcaoStub().ObterSolicitacaoAcaoDTO();
            await service.Criar(sol);

            Assert.Single(db.Solicitacoes.ToList());
        }

        // FIXME: vai mudar para usar solicitacaoAcaoRepositorio.CriarOuAtualizar()
        [Fact]
        public async Task Criar_QuandoEscolaJaTemSolicitacao_DeveRetornarExececao()
        {
            var escola = db.PopulaEscolas(1)[0];
            var solicitacao = new SolicitacaoAcaoStub().ObterSolicitacaoAcaoDTO();
            escola.Codigo = 1234;
            solicitacao.EscolaCodigoInep = 1234;
            
            await service.Criar(solicitacao);
            // FIXME: vai apenas atualizar a data de realização da solicitação
            var e = await Assert.ThrowsAsync<Exception>(async () => await service.Criar(solicitacao));

            Assert.Equal("Já foi feita uma solicitação para essa escola", e.Message);
            Assert.Single(db.Solicitacoes);
        }

        public new void Dispose()
        {
            db.Clear();
        }
    }
}
