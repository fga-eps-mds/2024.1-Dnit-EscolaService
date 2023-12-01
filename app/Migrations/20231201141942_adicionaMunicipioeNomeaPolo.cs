using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace app.Migrations
{
    /// <inheritdoc />
    public partial class adicionaMunicipioeNomeaPolo : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "MunicipioId",
                table: "Polos",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "Nome",
                table: "Polos",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_Polos_MunicipioId",
                table: "Polos",
                column: "MunicipioId");

            migrationBuilder.AddForeignKey(
                name: "FK_Polos_Municipios_MunicipioId",
                table: "Polos",
                column: "MunicipioId",
                principalTable: "Municipios",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Polos_Municipios_MunicipioId",
                table: "Polos");

            migrationBuilder.DropIndex(
                name: "IX_Polos_MunicipioId",
                table: "Polos");

            migrationBuilder.DropColumn(
                name: "MunicipioId",
                table: "Polos");

            migrationBuilder.DropColumn(
                name: "Nome",
                table: "Polos");
        }
    }
}
