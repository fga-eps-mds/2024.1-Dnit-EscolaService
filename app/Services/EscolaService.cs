using service.Interfaces;
using Microsoft.VisualBasic.FileIO;
using Newtonsoft.Json.Linq;
using app.Entidades;
using EnumsNET;
using api;
using app.Repositorios.Interfaces;
using api.Escolas;
using System.Data;
using app.util;
using Microsoft.AspNetCore.Mvc;
using System.Text;

namespace app.Services
{
    public class EscolaService : IEscolaService
    {
        private readonly IEscolaRepositorio escolaRepositorio;
        private readonly IMunicipioRepositorio municipioRepositorio;
        private readonly IPoloRepositorio poloRepositorio;
        private readonly ISolicitacaoAcaoRepositorio solicitacaoAcaoRepositorio;
        private readonly IRanqueService ranqueService;
        private readonly ModelConverter modelConverter;
        private readonly AppDbContext dbContext;

        public EscolaService(
            IEscolaRepositorio escolaRepositorio,
            IMunicipioRepositorio municipioRepositorio,
            ISolicitacaoAcaoRepositorio solicitacaoAcaoRepositorio,
            IRanqueService ranqueService,
            ModelConverter modelConverter,
            AppDbContext dbContext,
            IPoloRepositorio poloRepositorio
        )
        {
            this.escolaRepositorio = escolaRepositorio;
            this.municipioRepositorio = municipioRepositorio;
            this.solicitacaoAcaoRepositorio = solicitacaoAcaoRepositorio;
            this.ranqueService = ranqueService;
            this.modelConverter = modelConverter;
            this.dbContext = dbContext;
            this.poloRepositorio = poloRepositorio;
        }

        public bool SuperaTamanhoMaximo(MemoryStream planilha)
        {
            using (var reader = new StreamReader(planilha))
            {
                int tamanho_max = 5000;
                int quantidade_escolas = -1;

                while (reader.ReadLine() != null) { quantidade_escolas++; }

                return quantidade_escolas > tamanho_max;
            }
        }
        
        private async Task SetarPoloMaisProximo(Escola escola)
        {
            (Polo? poloMaisProximo, double distanciaPolo) = await CalcularPoloMaisProximo(escola);
            escola.Polo = poloMaisProximo;
            escola.DistanciaPolo = distanciaPolo;
        }
        
        private async Task<(Polo?, double)> CalcularPoloMaisProximo(Escola escola)
        {
            var polos = await poloRepositorio.ListarAsync();
            var (poloMaisProximo, distancia) = escola.CalcularPoloMaisProximo(polos);
            
            return (poloMaisProximo, distancia == null ? 0 : distancia.GetValueOrDefault());
        }

        public async Task CadastrarAsync(CadastroEscolaData cadastroEscolaData)
        {
            var municipioId = cadastroEscolaData.IdMunicipio ?? throw new ApiException(ErrorCodes.MunicipioNaoEncontrado);
            var municipio = await municipioRepositorio.ObterPorIdAsync(municipioId);

            var escola = escolaRepositorio.Criar(cadastroEscolaData, municipio);

            var solicitacao = await solicitacaoAcaoRepositorio.ObterPorCodigoInepdAsync(escola.Codigo);
            if (solicitacao != null)
            {
                AssociarSolicitacaoAEscola(solicitacao, escola);
            }

            cadastroEscolaData.IdEtapasDeEnsino
                ?.Select(e => escolaRepositorio.AdicionarEtapaEnsino(escola, (EtapaEnsino)e))
                ?.ToList();

            await SetarPoloMaisProximo(escola);
            // FIXME: seria melhor que fosse pedido apenas para calcular apenas
            // os UPSs das escolas recém adicionadas.
            await ranqueService.CalcularNovoRanqueAsync();

            await dbContext.SaveChangesAsync();
        }

        /// <summary>
        /// Ao criar uma escola a partir de uma solicitação lá no frontend, pode
        /// ser que o usuário preencha algumas informações na Escola que são
        /// diferentes na Solicitação. Para não haver inconsistência de dados,
        /// foi escolhido que as informações da Escola sobrescrevem as informações
        /// da Solicitação.
        /// </summary>
        private static void AssociarSolicitacaoAEscola(SolicitacaoAcao solicitacao, Escola escola)
        {
            escola.Solicitacao = solicitacao;
            solicitacao.EscolaId = escola.Id;
            solicitacao.TotalAlunos = escola.TotalAlunos;

            // TODO: tornar solicitacao.EscolaMunicipioId opicional?
            // FIXME: Seria melhor que UF e Município fossem obrigatórios em Escola e Solicitação
            if (escola.MunicipioId != null)
                solicitacao.EscolaMunicipioId = (int)escola.MunicipioId;
            if (escola.Uf != null)
                solicitacao.EscolaUf = (UF)escola.Uf;
        }

        public async Task<List<string>> CadastrarAsync(MemoryStream planilha)
        {
            var escolasNovas = new List<string>();

            using (var reader = new StreamReader(planilha))
            using (var parser = new TextFieldParser(reader))
            {
                parser.TextFieldType = FieldType.Delimited;
                parser.SetDelimiters(";");

                var primeiralinha = false;

                while (!parser.EndOfData)
                {
                    try
                    {
                        string[] linha = parser.ReadFields()!;
                        if (!primeiralinha)
                        {
                            primeiralinha = true;
                            continue;
                        }

                        var escolaNova = await criarEscolaPorLinhaAsync(linha);

                        if (escolaNova == null)
                        {
                            continue;
                        }

                        await dbContext.SaveChangesAsync();

                        escolasNovas.Add(escolaNova.Nome);
                    }
                    catch (FormatException)
                    {
                        throw new FormatException("Planilha com formato incompatível.");
                    }
                }
            }

            // FIXME: seria melhor que fosse pedido apenas para calcular apenas
            // os UPSs das escolas recém adicionadas.
            await ranqueService.CalcularNovoRanqueAsync();

            return escolasNovas;
        }

        public async Task AtualizarAsync(Escola escola, EscolaModel data)
        {
            escola.Nome = data.NomeEscola;
            escola.Codigo = data.CodigoEscola;
            escola.Cep = data.Cep;
            escola.Endereco = data.Endereco;
            escola.Latitude = data.Latitude ?? "";
            escola.Longitude = data.Longitude ?? "";
            escola.TotalAlunos = data.NumeroTotalDeAlunos ?? 0;
            escola.TotalDocentes = data.NumeroTotalDeDocentes;
            escola.Telefone = data.Telefone;
            escola.Rede = data.Rede!.Value;
            escola.Uf = data.Uf;
            escola.Localizacao = data.Localizacao;
            escola.MunicipioId = data.IdMunicipio;
            escola.Porte = data.Porte;
            escola.DataAtualizacao = DateTimeOffset.Now;
            await SetarPoloMaisProximo(escola);
            
            atualizarEtapasEnsino(escola, data.EtapasEnsino!);
            await dbContext.SaveChangesAsync();
        }

        private void atualizarEtapasEnsino(Escola escola, List<EtapaEnsino> etapas)
        {
            var etapasExistentes = escola.EtapasEnsino!.Select(e => e.EtapaEnsino).ToList();
            var etapasDeletadas = escola.EtapasEnsino!.Where(e => !etapas.Contains(e.EtapaEnsino));
            var etapasNovas = etapas.Where(e => !etapasExistentes.Contains(e));

            foreach (var etapa in etapasDeletadas)
            {
                dbContext.Remove(etapa);
            }
            foreach (var etapa in etapasNovas)
            {
                escolaRepositorio.AdicionarEtapaEnsino(escola, etapa);
            }
        }

        public async Task ExcluirAsync(Guid id)
        {
            var escola = await escolaRepositorio.ObterPorIdAsync(id);
            dbContext.Remove(escola);
            await dbContext.SaveChangesAsync();
        }

        public async Task RemoverSituacaoAsync(Guid id)
        {
            var escola = await escolaRepositorio.ObterPorIdAsync(id);
            escola.Situacao = null;
            await dbContext.SaveChangesAsync();
        }

        public async Task<ListaEscolaPaginada<EscolaCorretaModel>> ListarPaginadaAsync(PesquisaEscolaFiltro filtro)
        {
            var listaPaginadaEscolas = await escolaRepositorio.ListarPaginadaAsync(filtro);
            var escolasCorretas = listaPaginadaEscolas.Items.ConvertAll(modelConverter.ToModel);
            return new ListaEscolaPaginada<EscolaCorretaModel>(escolasCorretas, listaPaginadaEscolas.Pagina, listaPaginadaEscolas.ItemsPorPagina, listaPaginadaEscolas.Total);
        }

        public async Task<string?> ObterCodigoMunicipioPorCEPAsync(string cep)
        {
            var url = $"https://viacep.com.br/ws/{cep}/json/";

            using (var httpClient = new HttpClient())
            {
                try
                {
                    var response = await httpClient.GetAsync(url);

                    if (response.IsSuccessStatusCode)
                    {
                        var conteudo = await response.Content.ReadAsStringAsync();
                        dynamic resultado = JObject.Parse(conteudo);

                        return resultado.ibge;
                    }
                    else
                    {
                        return null;
                    }
                }
                catch (Exception)
                {
                    return null;
                }
            }
        }

        private List<EtapaEnsino> etapasParaIds(Dictionary<string, EtapaEnsino> etapas, string coluna_etapas)
        {
            var etapas_separadas = coluna_etapas.Split(',').Select(item => item.Trim());
            var resultado = etapas_separadas.Select(e => etapas.GetValueOrDefault(e.ToLower())).Where(e => e != default(EtapaEnsino)).ToList();
            return resultado;
        }

        public async Task AlterarDadosEscolaAsync(AtualizarDadosEscolaData dados)
        {
            var escola = await escolaRepositorio.ObterPorIdAsync(dados.IdEscola, incluirEtapas: true);

            escola.DataAtualizacao = DateTime.Now;
            escola.Telefone = dados.Telefone;
            escola.Longitude = dados.Longitude;
            escola.Latitude = dados.Latitude;
            escola.TotalAlunos = dados.NumeroTotalDeAlunos;
            escola.TotalDocentes = dados.NumeroTotalDeDocentes;
            escola.Observacao = dados.Observacao;
            escola.Situacao = (Situacao?)dados.IdSituacao;

            var (poloMaisProximo, distanciaSuper) = await
                CalcularPoloMaisProximo(escola);

            escola.Polo = poloMaisProximo;
            escola.PoloId = poloMaisProximo?.Id;
            escola.DistanciaPolo = distanciaSuper;
            
            atualizarEtapasEnsino(escola, dados.IdEtapasDeEnsino.ConvertAll(e => (EtapaEnsino)e));

            await dbContext.SaveChangesAsync();
        }

        private string obterValorLinha(string[] linha, Coluna coluna)
        {
            return linha[(int)coluna];
        }

        private async Task<Escola?> criarEscolaPorLinhaAsync(string[] linha)
        {
            var redes = Enum.GetValues<Rede>().ToDictionary(r => r.ToString().ToLower());
            var localizacoes = Enum.GetValues<Localizacao>().ToDictionary(l => l.ToString().ToLower());
            var ufs = Enum.GetValues<UF>().ToDictionary(uf => uf.ToString().ToLower());
            var portes = Enum.GetValues<Porte>()
                .ToDictionary(p => p.AsString(EnumFormat.Description)?.ToLower()
                                    ?? throw new NotImplementedException($"Enum ${nameof(Porte)} deve ter descrição"));
            var etapas = Enum.GetValues<EtapaEnsino>()
                .ToDictionary(e => e.AsString(EnumFormat.Description)?.ToLower()
                                    ?? throw new NotImplementedException($"Enum {nameof(EtapaEnsino)} deve ter descrição"));

            var escola = new EscolaModel()
            {
                CodigoEscola = int.Parse(obterValorLinha(linha, Coluna.CodigoInep)),
                NomeEscola = obterValorLinha(linha, Coluna.NomeEscola),
                Uf = ufs.GetValueOrDefault(obterValorLinha(linha, Coluna.Uf).ToLower()),
                Rede = redes.GetValueOrDefault(obterValorLinha(linha, Coluna.Rede).ToLower()),
                Porte = portes.GetValueOrDefault(obterValorLinha(linha, Coluna.Porte).ToLower()),
                Localizacao = localizacoes.GetValueOrDefault(obterValorLinha(linha, Coluna.Localizacao).ToLower()),
                Endereco = obterValorLinha(linha, Coluna.Endereco),
                Cep = obterValorLinha(linha, Coluna.Cep),
                Latitude = obterValorLinha(linha, Coluna.Latitude),
                Longitude = obterValorLinha(linha, Coluna.Longitude),
                Telefone = obterValorLinha(linha, Coluna.Dddd) + obterValorLinha(linha, Coluna.Telefone),
                NumeroTotalDeDocentes = int.Parse(obterValorLinha(linha, Coluna.QtdDocentes)),
                EtapasEnsino = etapasParaIds(etapas, obterValorLinha(linha, Coluna.EtapasEnsino)),
                IdMunicipio = null,
            };

            for (int i = (int)Coluna.QtdEnsinoInfantil; i <= (int)Coluna.QtdEnsinoFund9Ano; i++)
            {
                int quantidade;
                if (int.TryParse(linha[i], out quantidade)) escola.NumeroTotalDeAlunos += quantidade;
            }

            var municipio = await ObterCodigoMunicipioPorCEPAsync(escola.Cep);
            int codigoMunicipio;
            if (int.TryParse(municipio, out codigoMunicipio))
            {
                escola.IdMunicipio = int.Parse(municipio);
            }

            validaDadosCadastro(escola, obterValorLinha(linha, Coluna.EtapasEnsino));

            var escolaExistente = await escolaRepositorio.ObterPorCodigoAsync(escola.CodigoEscola);
            if (escolaExistente != default)
            {
                await AtualizarAsync(escolaExistente, escola);
                return null;
            }
            
            var escolaNova = escolaRepositorio.Criar(escola);
            await SetarPoloMaisProximo(escolaNova);
            foreach (var etapa in escola.EtapasEnsino)
            {
                escolaRepositorio.AdicionarEtapaEnsino(escolaNova, etapa);
            }


            return escolaNova;
        }

        private void validaDadosCadastro(EscolaModel escola, string etapas)
        {
            if (escola.IdMunicipio == null)
            {
                throw new ArgumentNullException("Cep", "Erro. A leitura do arquivo parou na escola: " + escola.NomeEscola + ", CEP inválido!");
            }
            if (escola.EtapasEnsino?.Count == 0 || escola.EtapasEnsino?.Count != etapas.Split(",").Count())
            {
                throw new ArgumentNullException("EtapasEnsino", "Erro. A leitura do arquivo parou na escola: " + escola.NomeEscola + ", descrição das etapas de ensino inválida!");
            }

            if (escola.Rede == default(Rede))
            {
                throw new ArgumentNullException("Rede", "Erro. A leitura do arquivo parou na escola: " + escola.NomeEscola + ", rede inválida!");
            }

            if (escola.Uf == default(UF))
            {
                throw new ArgumentNullException("Uf", "Erro. A leitura do arquivo parou na escola: " + escola.NomeEscola + ", UF inválida!");
            }

            if (escola.Localizacao == default(Localizacao))
            {
                throw new ArgumentNullException("Localizacao", "Erro. A leitura do arquivo parou na escola: " + escola.NomeEscola + ", localização inválida!");
            }

            if (escola.Porte == default(Porte))
            {
                throw new ArgumentNullException("Porte", "Erro. A leitura do arquivo parou na escola: " + escola.NomeEscola + ", descrição do porte inválida!");
            }
        }

    public async Task<FileResult> ExportarEscolasAsync()
        {
            var escolas = await escolaRepositorio.ListarAsync();
            var builder = new StringBuilder("");
            var delimiter = ";";
            builder.AppendLine(string.Join(delimiter, Escola.SerializeHeaders()));

            foreach (var escola in escolas)
            {
                string formatDistanciaPolo = escola.DistanciaPolo.ToString();
                formatDistanciaPolo = formatDistanciaPolo.Replace(".", ",");
                formatDistanciaPolo = $"\"{formatDistanciaPolo}\"";
                builder.AppendLine($"{CsvSerializer.Serialize(escola, delimiter)}; {formatDistanciaPolo}");
            }

            var bytes = Encoding.UTF8.GetPreamble().Concat(Encoding.UTF8.GetBytes(builder.ToString())).ToArray();
            return new FileContentResult(bytes, "text/csv")
            {
                FileDownloadName = $"escolas.csv",
            };
        }
    }

    enum Coluna
    {
        AnoSenso = 0,
        Id = 1,
        CodigoInep = 2,
        NomeEscola = 3,
        Rede = 4,
        Porte = 5,
        Endereco = 6,
        Cep = 7,
        Cidade = 8,
        Uf = 9,
        Localizacao = 10,
        Latitude = 11,
        Longitude = 12,
        Dddd = 13,
        Telefone = 14,
        EtapasEnsino = 15,
        QtdEnsinoInfantil = 16,
        QtdEnsinoFund1Ano = 17,
        QtdEnsinoFund2Ano = 18,
        QtdEnsinoFund3Ano = 19,
        QtdEnsinoFund4Ano = 20,
        QtdEnsinoFund5Ano = 21,
        QtdEnsinoFund6Ano = 22,
        QtdEnsinoFund7Ano = 23,
        QtdEnsinoFund8Ano = 24,
        QtdEnsinoFund9Ano = 25,
        QtdDocentes = 26,
    }
}


