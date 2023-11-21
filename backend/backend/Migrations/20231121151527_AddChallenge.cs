using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace backend.Migrations
{
    /// <inheritdoc />
    public partial class AddChallenge : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_user_equipe_equipe_team_id",
                table: "user_equipe");

            migrationBuilder.DropPrimaryKey(
                name: "PK_equipe",
                table: "equipe");

            migrationBuilder.RenameTable(
                name: "equipe",
                newName: "team");

            migrationBuilder.AddPrimaryKey(
                name: "PK_team",
                table: "team",
                column: "id");

            migrationBuilder.CreateTable(
                name: "challenge",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    name = table.Column<string>(type: "text", nullable: false),
                    description = table.Column<string>(type: "text", nullable: false),
                    creator_team_id = table.Column<Guid>(type: "uuid", nullable: false),
                    target_team_id = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_challenge", x => x.id);
                    table.ForeignKey(
                        name: "FK_challenge_team_creator_team_id",
                        column: x => x.creator_team_id,
                        principalTable: "team",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_challenge_team_target_team_id",
                        column: x => x.target_team_id,
                        principalTable: "team",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_challenge_creator_team_id",
                table: "challenge",
                column: "creator_team_id");

            migrationBuilder.CreateIndex(
                name: "IX_challenge_target_team_id",
                table: "challenge",
                column: "target_team_id");

            migrationBuilder.AddForeignKey(
                name: "FK_user_equipe_team_team_id",
                table: "user_equipe",
                column: "team_id",
                principalTable: "team",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_user_equipe_team_team_id",
                table: "user_equipe");

            migrationBuilder.DropTable(
                name: "challenge");

            migrationBuilder.DropPrimaryKey(
                name: "PK_team",
                table: "team");

            migrationBuilder.RenameTable(
                name: "team",
                newName: "equipe");

            migrationBuilder.AddPrimaryKey(
                name: "PK_equipe",
                table: "equipe",
                column: "id");

            migrationBuilder.AddForeignKey(
                name: "FK_user_equipe_equipe_team_id",
                table: "user_equipe",
                column: "team_id",
                principalTable: "equipe",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
