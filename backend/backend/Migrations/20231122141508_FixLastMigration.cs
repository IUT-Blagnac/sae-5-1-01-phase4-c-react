using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace backend.Migrations
{
    /// <inheritdoc />
    public partial class FixLastMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "id",
                table: "team_subject",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<int>(
                name: "id_sae",
                table: "character",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_character_id_sae",
                table: "character",
                column: "id_sae");

            migrationBuilder.AddForeignKey(
                name: "FK_character_sae_id_sae",
                table: "character",
                column: "id_sae",
                principalTable: "sae",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_character_sae_id_sae",
                table: "character");

            migrationBuilder.DropIndex(
                name: "IX_character_id_sae",
                table: "character");

            migrationBuilder.DropColumn(
                name: "id",
                table: "team_subject");

            migrationBuilder.DropColumn(
                name: "id_sae",
                table: "character");
        }
    }
}
