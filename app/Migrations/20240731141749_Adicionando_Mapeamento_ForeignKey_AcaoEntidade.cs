using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace app.Migrations
{
    /// <inheritdoc />
    public partial class Adicionando_Mapeamento_ForeignKey_AcaoEntidade : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "EscolasParticipantesPlanejamento",
                columns: table => new
                {
                    EscolaId = table.Column<Guid>(type: "uuid", nullable: false),
                    PlanejamentoMacroEscolaId = table.Column<Guid>(type: "uuid", nullable: false),
                    Id = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EscolasParticipantesPlanejamento", x => new { x.EscolaId, x.PlanejamentoMacroEscolaId });
                    table.ForeignKey(
                        name: "FK_EscolasParticipantesPlanejamento_Escolas_EscolaId",
                        column: x => x.EscolaId,
                        principalTable: "Escolas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_EscolasParticipantesPlanejamento_PlanejamentoMacroEscola_Pl~",
                        column: x => x.PlanejamentoMacroEscolaId,
                        principalTable: "PlanejamentoMacroEscola",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Acoes",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    EscolasParticipantesPlanejamentoId = table.Column<Guid>(type: "uuid", nullable: false),
                    EscolasParticipantesPlanejamentoEscolaId = table.Column<Guid>(type: "uuid", nullable: false),
                    EscolasParticipantesPlanejamentoPlanejamentoMacroEscolaId = table.Column<Guid>(type: "uuid", nullable: false),
                    EscolasParticipantesPlanejamentoEscolaId1 = table.Column<Guid>(type: "uuid", nullable: true),
                    EscolasParticipantesPlanejamentoPlanejamentoMacroEscolaId1 = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Acoes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Acoes_EscolasParticipantesPlanejamento_EscolasParticipantes~",
                        columns: x => new { x.EscolasParticipantesPlanejamentoEscolaId, x.EscolasParticipantesPlanejamentoPlanejamentoMacroEscolaId },
                        principalTable: "EscolasParticipantesPlanejamento",
                        principalColumns: new[] { "EscolaId", "PlanejamentoMacroEscolaId" },
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Acoes_EscolasParticipantesPlanejamento_EscolasParticipante~1",
                        columns: x => new { x.EscolasParticipantesPlanejamentoEscolaId1, x.EscolasParticipantesPlanejamentoPlanejamentoMacroEscolaId1 },
                        principalTable: "EscolasParticipantesPlanejamento",
                        principalColumns: new[] { "EscolaId", "PlanejamentoMacroEscolaId" });
                });

            migrationBuilder.CreateTable(
                name: "Atividades",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    AcaoId = table.Column<Guid>(type: "uuid", nullable: false),
                    AcaoId1 = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Atividades", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Atividades_Acoes_AcaoId",
                        column: x => x.AcaoId,
                        principalTable: "Acoes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Atividades_Acoes_AcaoId1",
                        column: x => x.AcaoId1,
                        principalTable: "Acoes",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Acoes_EscolasParticipantesPlanejamentoEscolaId_EscolasParti~",
                table: "Acoes",
                columns: new[] { "EscolasParticipantesPlanejamentoEscolaId", "EscolasParticipantesPlanejamentoPlanejamentoMacroEscolaId" });

            migrationBuilder.CreateIndex(
                name: "IX_Acoes_EscolasParticipantesPlanejamentoEscolaId1_EscolasPart~",
                table: "Acoes",
                columns: new[] { "EscolasParticipantesPlanejamentoEscolaId1", "EscolasParticipantesPlanejamentoPlanejamentoMacroEscolaId1" });

            migrationBuilder.CreateIndex(
                name: "IX_Atividades_AcaoId",
                table: "Atividades",
                column: "AcaoId");

            migrationBuilder.CreateIndex(
                name: "IX_Atividades_AcaoId1",
                table: "Atividades",
                column: "AcaoId1");

            migrationBuilder.CreateIndex(
                name: "IX_EscolasParticipantesPlanejamento_PlanejamentoMacroEscolaId",
                table: "EscolasParticipantesPlanejamento",
                column: "PlanejamentoMacroEscolaId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Atividades");

            migrationBuilder.DropTable(
                name: "Acoes");

            migrationBuilder.DropTable(
                name: "EscolasParticipantesPlanejamento");
        }
    }
}
