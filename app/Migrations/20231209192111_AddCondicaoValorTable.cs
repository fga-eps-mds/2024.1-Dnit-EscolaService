using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace app.Migrations
{
    /// <inheritdoc />
    public partial class AddCondicaoValorTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Valor",
                table: "FatorCondicoes");

            migrationBuilder.AddColumn<DateTime>(
                name: "DeleteTime",
                table: "FatorPriorizacoes",
                type: "timestamp with time zone",
                nullable: true);

            migrationBuilder.DropColumn(
                name: "Propriedade",
                table: "FatorCondicoes"
                );

            migrationBuilder.AddColumn<int>(
                name: "Propriedade",
                table: "FatorCondicoes",
                type: "integer",
                nullable: false
            );

            migrationBuilder.CreateTable(
                name: "CondicaoValores",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    FatorCondicaoId = table.Column<Guid>(type: "uuid", nullable: false),
                    Valor = table.Column<string>(type: "character varying(30)", maxLength: 30, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CondicaoValores", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CondicaoValores_FatorCondicoes_FatorCondicaoId",
                        column: x => x.FatorCondicaoId,
                        principalTable: "FatorCondicoes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CondicaoValores_FatorCondicaoId",
                table: "CondicaoValores",
                column: "FatorCondicaoId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CondicaoValores");

            migrationBuilder.DropColumn(
                name: "DeleteTime",
                table: "FatorPriorizacoes");

            migrationBuilder.AlterColumn<string>(
                name: "Propriedade",
                table: "FatorCondicoes",
                type: "character varying(30)",
                maxLength: 30,
                nullable: false,
                oldClrType: typeof(int),
                oldType: "integer",
                oldMaxLength: 30);

            migrationBuilder.AddColumn<string>(
                name: "Valor",
                table: "FatorCondicoes",
                type: "character varying(250)",
                maxLength: 250,
                nullable: false,
                defaultValue: "");
        }
    }
}
