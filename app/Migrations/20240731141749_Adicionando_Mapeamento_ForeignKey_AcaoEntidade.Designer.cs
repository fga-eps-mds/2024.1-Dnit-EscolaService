﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using app.Entidades;

#nullable disable

namespace app.Migrations
{
    [DbContext(typeof(AppDbContext))]
    [Migration("20240731141749_Adicionando_Mapeamento_ForeignKey_AcaoEntidade")]
    partial class Adicionando_Mapeamento_ForeignKey_AcaoEntidade
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.11")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("app.Entidades.Acao", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<Guid>("EscolasParticipantesPlanejamentoEscolaId")
                        .HasColumnType("uuid");

                    b.Property<Guid?>("EscolasParticipantesPlanejamentoEscolaId1")
                        .HasColumnType("uuid");

                    b.Property<Guid>("EscolasParticipantesPlanejamentoId")
                        .HasColumnType("uuid");

                    b.Property<Guid>("EscolasParticipantesPlanejamentoPlanejamentoMacroEscolaId")
                        .HasColumnType("uuid");

                    b.Property<Guid?>("EscolasParticipantesPlanejamentoPlanejamentoMacroEscolaId1")
                        .HasColumnType("uuid");

                    b.HasKey("Id");

                    b.HasIndex("EscolasParticipantesPlanejamentoEscolaId", "EscolasParticipantesPlanejamentoPlanejamentoMacroEscolaId");

                    b.HasIndex("EscolasParticipantesPlanejamentoEscolaId1", "EscolasParticipantesPlanejamentoPlanejamentoMacroEscolaId1");

                    b.ToTable("Acoes");
                });

            modelBuilder.Entity("app.Entidades.Atividade", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<Guid>("AcaoId")
                        .HasColumnType("uuid");

                    b.Property<Guid?>("AcaoId1")
                        .HasColumnType("uuid");

                    b.HasKey("Id");

                    b.HasIndex("AcaoId");

                    b.HasIndex("AcaoId1");

                    b.ToTable("Atividades");
                });

            modelBuilder.Entity("app.Entidades.CondicaoValor", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<Guid>("FatorCondicaoId")
                        .HasColumnType("uuid");

                    b.Property<string>("Valor")
                        .IsRequired()
                        .HasMaxLength(30)
                        .HasColumnType("character varying(30)");

                    b.HasKey("Id");

                    b.HasIndex("FatorCondicaoId");

                    b.ToTable("CondicaoValores");
                });

            modelBuilder.Entity("app.Entidades.CustoLogistico", b =>
                {
                    b.Property<int>("Custo")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Custo"));

                    b.Property<int?>("RaioMax")
                        .HasColumnType("integer");

                    b.Property<int>("RaioMin")
                        .HasColumnType("integer");

                    b.Property<int>("Valor")
                        .HasColumnType("integer");

                    b.HasKey("Custo");

                    b.ToTable("CustosLogisticos");
                });

            modelBuilder.Entity("app.Entidades.Escola", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("Cep")
                        .IsRequired()
                        .HasMaxLength(8)
                        .HasColumnType("character varying(8)");

                    b.Property<int>("Codigo")
                        .HasColumnType("integer");

                    b.Property<DateTime?>("DataAtualizacaoUtc")
                        .HasColumnType("timestamp with time zone");

                    b.Property<double>("DistanciaPolo")
                        .HasColumnType("double precision");

                    b.Property<string>("Endereco")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("character varying(200)");

                    b.Property<string>("Latitude")
                        .IsRequired()
                        .HasMaxLength(12)
                        .HasColumnType("character varying(12)");

                    b.Property<int?>("Localizacao")
                        .HasColumnType("integer");

                    b.Property<string>("Longitude")
                        .IsRequired()
                        .HasMaxLength(12)
                        .HasColumnType("character varying(12)");

                    b.Property<int?>("MunicipioId")
                        .HasColumnType("integer");

                    b.Property<string>("Nome")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("character varying(200)");

                    b.Property<string>("Observacao")
                        .HasMaxLength(500)
                        .HasColumnType("character varying(500)");

                    b.Property<int?>("PoloId")
                        .HasColumnType("integer");

                    b.Property<int?>("Porte")
                        .HasColumnType("integer");

                    b.Property<int>("Rede")
                        .HasColumnType("integer");

                    b.Property<int?>("Situacao")
                        .HasColumnType("integer");

                    b.Property<string>("Telefone")
                        .IsRequired()
                        .HasMaxLength(11)
                        .HasColumnType("character varying(11)");

                    b.Property<int>("TotalAlunos")
                        .HasColumnType("integer");

                    b.Property<int>("TotalDocentes")
                        .HasColumnType("integer");

                    b.Property<int?>("Uf")
                        .HasColumnType("integer");

                    b.Property<int>("Ups")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("MunicipioId");

                    b.HasIndex("PoloId");

                    b.ToTable("Escolas");
                });

            modelBuilder.Entity("app.Entidades.EscolaEtapaEnsino", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<Guid>("EscolaId")
                        .HasColumnType("uuid");

                    b.Property<int>("EtapaEnsino")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("EscolaId");

                    b.ToTable("EscolaEtapaEnsino");
                });

            modelBuilder.Entity("app.Entidades.EscolaRanque", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<Guid>("EscolaId")
                        .HasColumnType("uuid");

                    b.Property<int>("Pontuacao")
                        .HasColumnType("integer");

                    b.Property<int>("Posicao")
                        .HasColumnType("integer");

                    b.Property<int>("RanqueId")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("EscolaId");

                    b.HasIndex("RanqueId");

                    b.ToTable("EscolaRanques");
                });

            modelBuilder.Entity("app.Entidades.EscolasParticipantesPlanejamento", b =>
                {
                    b.Property<Guid>("EscolaId")
                        .HasColumnType("uuid");

                    b.Property<Guid>("PlanejamentoMacroEscolaId")
                        .HasColumnType("uuid");

                    b.Property<Guid>("Id")
                        .HasColumnType("uuid");

                    b.HasKey("EscolaId", "PlanejamentoMacroEscolaId");

                    b.HasIndex("PlanejamentoMacroEscolaId");

                    b.ToTable("EscolasParticipantesPlanejamento");
                });

            modelBuilder.Entity("app.Entidades.FatorCondicao", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<Guid>("FatorPriorizacaoId")
                        .HasColumnType("uuid");

                    b.Property<int>("Operador")
                        .HasColumnType("integer");

                    b.Property<int>("Propriedade")
                        .HasMaxLength(30)
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("FatorPriorizacaoId");

                    b.ToTable("FatorCondicoes");
                });

            modelBuilder.Entity("app.Entidades.FatorEscola", b =>
                {
                    b.Property<Guid>("FatorPriorizacaoId")
                        .HasColumnType("uuid");

                    b.Property<Guid>("EscolaId")
                        .HasColumnType("uuid");

                    b.Property<int>("Valor")
                        .HasColumnType("integer");

                    b.HasKey("FatorPriorizacaoId", "EscolaId");

                    b.HasIndex("EscolaId");

                    b.ToTable("FatorEscolas");
                });

            modelBuilder.Entity("app.Entidades.FatorPriorizacao", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<bool>("Ativo")
                        .HasColumnType("boolean");

                    b.Property<DateTime?>("DeleteTime")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("Nome")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("character varying(100)");

                    b.Property<int>("Peso")
                        .HasColumnType("integer");

                    b.Property<bool>("Primario")
                        .HasColumnType("boolean");

                    b.HasKey("Id");

                    b.ToTable("FatorPriorizacoes");
                });

            modelBuilder.Entity("app.Entidades.FatorRanque", b =>
                {
                    b.Property<Guid>("FatorPriorizacaoId")
                        .HasColumnType("uuid")
                        .HasColumnOrder(1);

                    b.Property<int>("RanqueId")
                        .HasColumnType("integer")
                        .HasColumnOrder(2);

                    b.HasKey("FatorPriorizacaoId", "RanqueId");

                    b.HasIndex("RanqueId");

                    b.ToTable("FatorRanques");
                });

            modelBuilder.Entity("app.Entidades.Municipio", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("Nome")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("character varying(50)");

                    b.Property<int>("Uf")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.ToTable("Municipios");
                });

            modelBuilder.Entity("app.Entidades.PlanejamentoMacro", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("AnoFim")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("AnoInicio")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("MesFim")
                        .HasColumnType("integer");

                    b.Property<int>("MesInicio")
                        .HasColumnType("integer");

                    b.Property<string>("Nome")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("character varying(200)");

                    b.Property<int>("QuantidadeAcoes")
                        .HasColumnType("integer");

                    b.Property<string>("Responsavel")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("PlanejamentoMacro");
                });

            modelBuilder.Entity("app.Entidades.PlanejamentoMacroEscola", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("Ano")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<Guid>("EscolaId")
                        .HasColumnType("uuid");

                    b.Property<int>("Mes")
                        .HasColumnType("integer");

                    b.Property<Guid>("PlanejamentoMacroId")
                        .HasColumnType("uuid");

                    b.HasKey("Id");

                    b.HasIndex("EscolaId");

                    b.HasIndex("PlanejamentoMacroId");

                    b.ToTable("PlanejamentoMacroEscola");
                });

            modelBuilder.Entity("app.Entidades.Polo", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("Cep")
                        .IsRequired()
                        .HasMaxLength(10)
                        .HasColumnType("character varying(10)");

                    b.Property<string>("Endereco")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Latitude")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Longitude")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("MunicipioId")
                        .HasColumnType("integer");

                    b.Property<string>("Nome")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("Uf")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("MunicipioId");

                    b.ToTable("Polos");
                });

            modelBuilder.Entity("app.Entidades.Ranque", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<int>("BateladasEmProgresso")
                        .HasColumnType("integer");

                    b.Property<DateTime?>("DataFimUtc")
                        .HasColumnType("timestamp with time zone");

                    b.Property<DateTime>("DataInicioUtc")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("Descricao")
                        .HasMaxLength(100)
                        .HasColumnType("character varying(100)");

                    b.HasKey("Id");

                    b.ToTable("Ranques");
                });

            modelBuilder.Entity("app.Entidades.SolicitacaoAcao", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<DateTime?>("DataRealizadaUtc")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("character varying(100)");

                    b.Property<int>("EscolaCodigoInep")
                        .HasColumnType("integer");

                    b.Property<Guid?>("EscolaId")
                        .HasColumnType("uuid");

                    b.Property<int>("EscolaMunicipioId")
                        .HasColumnType("integer");

                    b.Property<string>("EscolaNome")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("character varying(200)");

                    b.Property<int>("EscolaUf")
                        .HasColumnType("integer");

                    b.Property<string>("NomeSolicitante")
                        .IsRequired()
                        .HasMaxLength(150)
                        .HasColumnType("character varying(150)");

                    b.Property<string>("Observacoes")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("character varying(200)");

                    b.Property<string>("Telefone")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("character varying(20)");

                    b.Property<int>("TotalAlunos")
                        .HasColumnType("integer");

                    b.Property<string>("Vinculo")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("character varying(20)");

                    b.HasKey("Id");

                    b.HasIndex("EscolaId")
                        .IsUnique();

                    b.HasIndex("EscolaMunicipioId");

                    b.ToTable("Solicitacoes");
                });

            modelBuilder.Entity("app.Entidades.Acao", b =>
                {
                    b.HasOne("app.Entidades.EscolasParticipantesPlanejamento", "EscolasParticipantesPlanejamento")
                        .WithMany()
                        .HasForeignKey("EscolasParticipantesPlanejamentoEscolaId", "EscolasParticipantesPlanejamentoPlanejamentoMacroEscolaId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("app.Entidades.EscolasParticipantesPlanejamento", null)
                        .WithMany("EscolasParticipantesPlanejamentos")
                        .HasForeignKey("EscolasParticipantesPlanejamentoEscolaId1", "EscolasParticipantesPlanejamentoPlanejamentoMacroEscolaId1")
                        .HasConstraintName("FK_Acoes_EscolasParticipantesPlanejamento_EscolasParticipante~1");

                    b.Navigation("EscolasParticipantesPlanejamento");
                });

            modelBuilder.Entity("app.Entidades.Atividade", b =>
                {
                    b.HasOne("app.Entidades.Acao", "Acao")
                        .WithMany()
                        .HasForeignKey("AcaoId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("app.Entidades.Acao", null)
                        .WithMany("Atividade")
                        .HasForeignKey("AcaoId1");

                    b.Navigation("Acao");
                });

            modelBuilder.Entity("app.Entidades.CondicaoValor", b =>
                {
                    b.HasOne("app.Entidades.FatorCondicao", null)
                        .WithMany("Valores")
                        .HasForeignKey("FatorCondicaoId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("app.Entidades.Escola", b =>
                {
                    b.HasOne("app.Entidades.Municipio", "Municipio")
                        .WithMany()
                        .HasForeignKey("MunicipioId");

                    b.HasOne("app.Entidades.Polo", "Polo")
                        .WithMany()
                        .HasForeignKey("PoloId");

                    b.Navigation("Municipio");

                    b.Navigation("Polo");
                });

            modelBuilder.Entity("app.Entidades.EscolaEtapaEnsino", b =>
                {
                    b.HasOne("app.Entidades.Escola", "Escola")
                        .WithMany("EtapasEnsino")
                        .HasForeignKey("EscolaId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Escola");
                });

            modelBuilder.Entity("app.Entidades.EscolaRanque", b =>
                {
                    b.HasOne("app.Entidades.Escola", "Escola")
                        .WithMany()
                        .HasForeignKey("EscolaId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("app.Entidades.Ranque", "Ranque")
                        .WithMany("EscolaRanques")
                        .HasForeignKey("RanqueId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Escola");

                    b.Navigation("Ranque");
                });

            modelBuilder.Entity("app.Entidades.EscolasParticipantesPlanejamento", b =>
                {
                    b.HasOne("app.Entidades.Escola", "Escola")
                        .WithMany("EscolasParticipantesPlanejamentos")
                        .HasForeignKey("EscolaId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("app.Entidades.PlanejamentoMacroEscola", "PlanejamentoMacroEscola")
                        .WithMany("EscolasParticipantesPlanejamentos")
                        .HasForeignKey("PlanejamentoMacroEscolaId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Escola");

                    b.Navigation("PlanejamentoMacroEscola");
                });

            modelBuilder.Entity("app.Entidades.FatorCondicao", b =>
                {
                    b.HasOne("app.Entidades.FatorPriorizacao", null)
                        .WithMany("FatorCondicoes")
                        .HasForeignKey("FatorPriorizacaoId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("app.Entidades.FatorEscola", b =>
                {
                    b.HasOne("app.Entidades.Escola", "Escola")
                        .WithMany()
                        .HasForeignKey("EscolaId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("app.Entidades.FatorPriorizacao", "FatorPriorizacao")
                        .WithMany()
                        .HasForeignKey("FatorPriorizacaoId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Escola");

                    b.Navigation("FatorPriorizacao");
                });

            modelBuilder.Entity("app.Entidades.FatorRanque", b =>
                {
                    b.HasOne("app.Entidades.FatorPriorizacao", "FatorPriorizacao")
                        .WithMany()
                        .HasForeignKey("FatorPriorizacaoId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("app.Entidades.Ranque", "Ranque")
                        .WithMany()
                        .HasForeignKey("RanqueId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("FatorPriorizacao");

                    b.Navigation("Ranque");
                });

            modelBuilder.Entity("app.Entidades.PlanejamentoMacroEscola", b =>
                {
                    b.HasOne("app.Entidades.Escola", "Escola")
                        .WithMany()
                        .HasForeignKey("EscolaId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("app.Entidades.PlanejamentoMacro", "PlanejamentoMacro")
                        .WithMany("PlanejamentoMacroEscolas")
                        .HasForeignKey("PlanejamentoMacroId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Escola");

                    b.Navigation("PlanejamentoMacro");
                });

            modelBuilder.Entity("app.Entidades.Polo", b =>
                {
                    b.HasOne("app.Entidades.Municipio", "Municipio")
                        .WithMany()
                        .HasForeignKey("MunicipioId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Municipio");
                });

            modelBuilder.Entity("app.Entidades.SolicitacaoAcao", b =>
                {
                    b.HasOne("app.Entidades.Escola", "Escola")
                        .WithOne("Solicitacao")
                        .HasForeignKey("app.Entidades.SolicitacaoAcao", "EscolaId");

                    b.HasOne("app.Entidades.Municipio", "EscolaMunicipio")
                        .WithMany()
                        .HasForeignKey("EscolaMunicipioId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Escola");

                    b.Navigation("EscolaMunicipio");
                });

            modelBuilder.Entity("app.Entidades.Acao", b =>
                {
                    b.Navigation("Atividade");
                });

            modelBuilder.Entity("app.Entidades.Escola", b =>
                {
                    b.Navigation("EscolasParticipantesPlanejamentos");

                    b.Navigation("EtapasEnsino");

                    b.Navigation("Solicitacao");
                });

            modelBuilder.Entity("app.Entidades.EscolasParticipantesPlanejamento", b =>
                {
                    b.Navigation("EscolasParticipantesPlanejamentos");
                });

            modelBuilder.Entity("app.Entidades.FatorCondicao", b =>
                {
                    b.Navigation("Valores");
                });

            modelBuilder.Entity("app.Entidades.FatorPriorizacao", b =>
                {
                    b.Navigation("FatorCondicoes");
                });

            modelBuilder.Entity("app.Entidades.PlanejamentoMacro", b =>
                {
                    b.Navigation("PlanejamentoMacroEscolas");
                });

            modelBuilder.Entity("app.Entidades.PlanejamentoMacroEscola", b =>
                {
                    b.Navigation("EscolasParticipantesPlanejamentos");
                });

            modelBuilder.Entity("app.Entidades.Ranque", b =>
                {
                    b.Navigation("EscolaRanques");
                });
#pragma warning restore 612, 618
        }
    }
}
