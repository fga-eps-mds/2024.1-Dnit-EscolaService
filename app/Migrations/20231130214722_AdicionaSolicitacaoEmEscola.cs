using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace app.Migrations
{
    /// <inheritdoc />
    public partial class AdicionaSolicitacaoEmEscola : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Solicitacoes_EscolaId",
                table: "Solicitacoes");

            migrationBuilder.CreateIndex(
                name: "IX_Solicitacoes_EscolaId",
                table: "Solicitacoes",
                column: "EscolaId",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Solicitacoes_EscolaId",
                table: "Solicitacoes");

            migrationBuilder.CreateIndex(
                name: "IX_Solicitacoes_EscolaId",
                table: "Solicitacoes",
                column: "EscolaId");
        }
    }
}
