using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace backend.Migrations
{
    /// <inheritdoc />
    public partial class AddForeignKeySae : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "id_sae",
                table: "team",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_team_id_sae",
                table: "team",
                column: "id_sae");

            migrationBuilder.AddForeignKey(
                name: "FK_team_sae_id_sae",
                table: "team",
                column: "id_sae",
                principalTable: "sae",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_team_sae_id_sae",
                table: "team");

            migrationBuilder.DropIndex(
                name: "IX_team_id_sae",
                table: "team");

            migrationBuilder.DropColumn(
                name: "id_sae",
                table: "team");
        }
    }
}
