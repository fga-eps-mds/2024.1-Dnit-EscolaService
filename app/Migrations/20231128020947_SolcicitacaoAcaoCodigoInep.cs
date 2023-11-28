using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace app.Migrations
{
    /// <inheritdoc />
    public partial class SolcicitacaoAcaoCodigoInep : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Solicitacoes_Escolas_EscolaId",
                table: "Solicitacoes");

            migrationBuilder.AlterColumn<Guid>(
                name: "EscolaId",
                table: "Solicitacoes",
                type: "uuid",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uuid");

            migrationBuilder.AddColumn<int>(
                name: "EscolaCodigoInep",
                table: "Solicitacoes",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<bool>(
                name: "EscolaJaCadastrada",
                table: "Solicitacoes",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddForeignKey(
                name: "FK_Solicitacoes_Escolas_EscolaId",
                table: "Solicitacoes",
                column: "EscolaId",
                principalTable: "Escolas",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Solicitacoes_Escolas_EscolaId",
                table: "Solicitacoes");

            migrationBuilder.DropColumn(
                name: "EscolaCodigoInep",
                table: "Solicitacoes");

            migrationBuilder.DropColumn(
                name: "EscolaJaCadastrada",
                table: "Solicitacoes");

            migrationBuilder.AlterColumn<Guid>(
                name: "EscolaId",
                table: "Solicitacoes",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uuid",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Solicitacoes_Escolas_EscolaId",
                table: "Solicitacoes",
                column: "EscolaId",
                principalTable: "Escolas",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
