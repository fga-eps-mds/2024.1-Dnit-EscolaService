using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace app.Migrations
{
    /// <inheritdoc />
    public partial class FatorEscola : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "FatorEscolas",
                columns: table => new
                {
                    FatorPriorizacaoId = table.Column<Guid>(type: "uuid", nullable: false),
                    EscolaId = table.Column<Guid>(type: "uuid", nullable: false),
                    Valor = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FatorEscolas", x => new { x.FatorPriorizacaoId, x.EscolaId });
                    table.ForeignKey(
                        name: "FK_FatorEscolas_Escolas_EscolaId",
                        column: x => x.EscolaId,
                        principalTable: "Escolas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_FatorEscolas_FatorPriorizacoes_FatorPriorizacaoId",
                        column: x => x.FatorPriorizacaoId,
                        principalTable: "FatorPriorizacoes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_FatorEscolas_EscolaId",
                table: "FatorEscolas",
                column: "EscolaId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "FatorEscolas");
        }
    }
}
