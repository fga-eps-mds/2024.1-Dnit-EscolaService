using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace app.Migrations
{
    /// <inheritdoc />
    public partial class AddFatorRanque : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "FatorRanques",
                columns: table => new
                {
                    FatorPriorizacaoId = table.Column<Guid>(type: "uuid", nullable: false),
                    RanqueId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FatorRanques", x => new { x.FatorPriorizacaoId, x.RanqueId });
                    table.ForeignKey(
                        name: "FK_FatorRanques_FatorPriorizacoes_FatorPriorizacaoId",
                        column: x => x.FatorPriorizacaoId,
                        principalTable: "FatorPriorizacoes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_FatorRanques_Ranques_RanqueId",
                        column: x => x.RanqueId,
                        principalTable: "Ranques",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_FatorRanques_RanqueId",
                table: "FatorRanques",
                column: "RanqueId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "FatorRanques");
        }
    }
}
