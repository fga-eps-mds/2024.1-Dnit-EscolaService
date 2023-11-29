using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace app.Migrations
{
    /// <inheritdoc />
    public partial class ModificaSolicitacaoAcao : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "EscolaJaCadastrada",
                table: "Solicitacoes");

            migrationBuilder.AddColumn<int>(
                name: "EscolaMunicipioId",
                table: "Solicitacoes",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "EscolaNome",
                table: "Solicitacoes",
                type: "character varying(200)",
                maxLength: 200,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "EscolaUf",
                table: "Solicitacoes",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "Vinculo",
                table: "Solicitacoes",
                type: "character varying(20)",
                maxLength: 20,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<Guid>(
                name: "SolicitacaoAcaoId",
                table: "EscolaEtapaEnsino",
                type: "uuid",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Solicitacoes_EscolaMunicipioId",
                table: "Solicitacoes",
                column: "EscolaMunicipioId");

            migrationBuilder.CreateIndex(
                name: "IX_EscolaEtapaEnsino_SolicitacaoAcaoId",
                table: "EscolaEtapaEnsino",
                column: "SolicitacaoAcaoId");

            migrationBuilder.AddForeignKey(
                name: "FK_EscolaEtapaEnsino_Solicitacoes_SolicitacaoAcaoId",
                table: "EscolaEtapaEnsino",
                column: "SolicitacaoAcaoId",
                principalTable: "Solicitacoes",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Solicitacoes_Municipios_EscolaMunicipioId",
                table: "Solicitacoes",
                column: "EscolaMunicipioId",
                principalTable: "Municipios",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_EscolaEtapaEnsino_Solicitacoes_SolicitacaoAcaoId",
                table: "EscolaEtapaEnsino");

            migrationBuilder.DropForeignKey(
                name: "FK_Solicitacoes_Municipios_EscolaMunicipioId",
                table: "Solicitacoes");

            migrationBuilder.DropIndex(
                name: "IX_Solicitacoes_EscolaMunicipioId",
                table: "Solicitacoes");

            migrationBuilder.DropIndex(
                name: "IX_EscolaEtapaEnsino_SolicitacaoAcaoId",
                table: "EscolaEtapaEnsino");

            migrationBuilder.DropColumn(
                name: "EscolaMunicipioId",
                table: "Solicitacoes");

            migrationBuilder.DropColumn(
                name: "EscolaNome",
                table: "Solicitacoes");

            migrationBuilder.DropColumn(
                name: "EscolaUf",
                table: "Solicitacoes");

            migrationBuilder.DropColumn(
                name: "Vinculo",
                table: "Solicitacoes");

            migrationBuilder.DropColumn(
                name: "SolicitacaoAcaoId",
                table: "EscolaEtapaEnsino");

            migrationBuilder.AddColumn<bool>(
                name: "EscolaJaCadastrada",
                table: "Solicitacoes",
                type: "boolean",
                nullable: false,
                defaultValue: false);
        }
    }
}
