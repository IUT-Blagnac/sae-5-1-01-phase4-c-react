using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace backend.Migrations
{
    /// <inheritdoc />
    public partial class FixPrimaryKeys : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_team_subject_subject_subject_id",
                table: "team_subject");

            migrationBuilder.DropForeignKey(
                name: "FK_team_subject_team_team_id",
                table: "team_subject");

            migrationBuilder.DropForeignKey(
                name: "FK_user_role_user_id_role",
                table: "user");

            migrationBuilder.DropTable(
                name: "role_user");

            migrationBuilder.DropPrimaryKey(
                name: "PK_team_wish",
                table: "team_wish");

            migrationBuilder.DropIndex(
                name: "IX_team_wish_id_team",
                table: "team_wish");

            migrationBuilder.DropPrimaryKey(
                name: "PK_team_subject",
                table: "team_subject");

            migrationBuilder.DropIndex(
                name: "IX_team_subject_team_id",
                table: "team_subject");

            migrationBuilder.DropPrimaryKey(
                name: "PK_sae_group",
                table: "sae_group");

            migrationBuilder.DropIndex(
                name: "IX_sae_group_id_sae",
                table: "sae_group");

            migrationBuilder.DropPrimaryKey(
                name: "PK_sae_coach",
                table: "sae_coach");

            migrationBuilder.DropIndex(
                name: "IX_sae_coach_id_sae",
                table: "sae_coach");

            migrationBuilder.DropPrimaryKey(
                name: "PK_character_skill",
                table: "character_skill");

            migrationBuilder.DropIndex(
                name: "IX_character_skill_id_character",
                table: "character_skill");

            migrationBuilder.DropColumn(
                name: "id",
                table: "team_wish");

            migrationBuilder.DropColumn(
                name: "subject_id",
                table: "team_subject");

            migrationBuilder.DropColumn(
                name: "id",
                table: "sae_group");

            migrationBuilder.DropColumn(
                name: "id",
                table: "sae_coach");

            migrationBuilder.DropColumn(
                name: "id",
                table: "character_skill");

            migrationBuilder.RenameColumn(
                name: "id",
                table: "team_subject",
                newName: "id_subject");

            migrationBuilder.RenameColumn(
                name: "team_id",
                table: "team_subject",
                newName: "id_team");

            migrationBuilder.AddPrimaryKey(
                name: "PK_team_wish",
                table: "team_wish",
                columns: new[] { "id_team", "id_subject" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_team_subject",
                table: "team_subject",
                columns: new[] { "id_team", "id_subject" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_sae_group",
                table: "sae_group",
                columns: new[] { "id_sae", "id_group" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_sae_coach",
                table: "sae_coach",
                columns: new[] { "id_sae", "id_coach" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_character_skill",
                table: "character_skill",
                columns: new[] { "id_character", "id_skill" });

            migrationBuilder.CreateTable(
                name: "role",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    name = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_role", x => x.id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_team_subject_id_subject",
                table: "team_subject",
                column: "id_subject");

            migrationBuilder.AddForeignKey(
                name: "FK_team_subject_subject_id_subject",
                table: "team_subject",
                column: "id_subject",
                principalTable: "subject",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_team_subject_team_id_team",
                table: "team_subject",
                column: "id_team",
                principalTable: "team",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_user_role_id_role",
                table: "user",
                column: "id_role",
                principalTable: "role",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_team_subject_subject_id_subject",
                table: "team_subject");

            migrationBuilder.DropForeignKey(
                name: "FK_team_subject_team_id_team",
                table: "team_subject");

            migrationBuilder.DropForeignKey(
                name: "FK_user_role_id_role",
                table: "user");

            migrationBuilder.DropTable(
                name: "role");

            migrationBuilder.DropPrimaryKey(
                name: "PK_team_wish",
                table: "team_wish");

            migrationBuilder.DropPrimaryKey(
                name: "PK_team_subject",
                table: "team_subject");

            migrationBuilder.DropIndex(
                name: "IX_team_subject_id_subject",
                table: "team_subject");

            migrationBuilder.DropPrimaryKey(
                name: "PK_sae_group",
                table: "sae_group");

            migrationBuilder.DropPrimaryKey(
                name: "PK_sae_coach",
                table: "sae_coach");

            migrationBuilder.DropPrimaryKey(
                name: "PK_character_skill",
                table: "character_skill");

            migrationBuilder.RenameColumn(
                name: "id_subject",
                table: "team_subject",
                newName: "id");

            migrationBuilder.RenameColumn(
                name: "id_team",
                table: "team_subject",
                newName: "team_id");

            migrationBuilder.AddColumn<Guid>(
                name: "id",
                table: "team_wish",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "subject_id",
                table: "team_subject",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "id",
                table: "sae_group",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "id",
                table: "sae_coach",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "id",
                table: "character_skill",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddPrimaryKey(
                name: "PK_team_wish",
                table: "team_wish",
                column: "id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_team_subject",
                table: "team_subject",
                columns: new[] { "subject_id", "team_id" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_sae_group",
                table: "sae_group",
                column: "id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_sae_coach",
                table: "sae_coach",
                column: "id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_character_skill",
                table: "character_skill",
                column: "id");

            migrationBuilder.CreateTable(
                name: "role_user",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    name = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_role_user", x => x.id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_team_wish_id_team",
                table: "team_wish",
                column: "id_team");

            migrationBuilder.CreateIndex(
                name: "IX_team_subject_team_id",
                table: "team_subject",
                column: "team_id");

            migrationBuilder.CreateIndex(
                name: "IX_sae_group_id_sae",
                table: "sae_group",
                column: "id_sae");

            migrationBuilder.CreateIndex(
                name: "IX_sae_coach_id_sae",
                table: "sae_coach",
                column: "id_sae");

            migrationBuilder.CreateIndex(
                name: "IX_character_skill_id_character",
                table: "character_skill",
                column: "id_character");

            migrationBuilder.AddForeignKey(
                name: "FK_team_subject_subject_subject_id",
                table: "team_subject",
                column: "subject_id",
                principalTable: "subject",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_team_subject_team_team_id",
                table: "team_subject",
                column: "team_id",
                principalTable: "team",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_user_role_user_id_role",
                table: "user",
                column: "id_role",
                principalTable: "role_user",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
