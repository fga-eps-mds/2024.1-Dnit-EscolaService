using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace app.Migrations
{
    /// <inheritdoc />
    public partial class AlteraSolicitacao : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Solicitacoes_Municipios_EscolaMunicipioId",
                table: "Solicitacoes");

            migrationBuilder.AlterColumn<int>(
                name: "EscolaMunicipioId",
                table: "Solicitacoes",
                type: "integer",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "TotalAlunos",
                table: "Solicitacoes",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddForeignKey(
                name: "FK_Solicitacoes_Municipios_EscolaMunicipioId",
                table: "Solicitacoes",
                column: "EscolaMunicipioId",
                principalTable: "Municipios",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Solicitacoes_Municipios_EscolaMunicipioId",
                table: "Solicitacoes");

            migrationBuilder.DropColumn(
                name: "TotalAlunos",
                table: "Solicitacoes");

            migrationBuilder.AlterColumn<int>(
                name: "EscolaMunicipioId",
                table: "Solicitacoes",
                type: "integer",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AddForeignKey(
                name: "FK_Solicitacoes_Municipios_EscolaMunicipioId",
                table: "Solicitacoes",
                column: "EscolaMunicipioId",
                principalTable: "Municipios",
                principalColumn: "Id");
        }
    }
}
