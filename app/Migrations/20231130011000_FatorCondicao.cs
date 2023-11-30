using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace app.Migrations
{
    /// <inheritdoc />
    public partial class FatorCondicao : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "FatorCondicoes",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Propriedade = table.Column<string>(type: "character varying(30)", maxLength: 30, nullable: false),
                    Operador = table.Column<int>(type: "integer", nullable: false),
                    Valor = table.Column<string>(type: "character varying(250)", maxLength: 250, nullable: false),
                    FatorPriorizacaoId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FatorCondicoes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_FatorCondicoes_FatorPriorizacoes_FatorPriorizacaoId",
                        column: x => x.FatorPriorizacaoId,
                        principalTable: "FatorPriorizacoes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_FatorCondicoes_FatorPriorizacaoId",
                table: "FatorCondicoes",
                column: "FatorPriorizacaoId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "FatorCondicoes");
        }
    }
}
