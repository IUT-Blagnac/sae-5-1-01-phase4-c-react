using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace backend.Migrations
{
    /// <inheritdoc />
    public partial class AddCharacterSkills : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "id_groupe",
                table: "user",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Saeid",
                table: "subject",
                type: "integer",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "character",
                columns: table => new
                {
                    id_character = table.Column<Guid>(type: "uuid", nullable: false),
                    name = table.Column<string>(type: "text", nullable: false),
                    id_user = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_character", x => x.id_character);
                    table.ForeignKey(
                        name: "FK_character_user_id_user",
                        column: x => x.id_user,
                        principalTable: "user",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "prom",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    year = table.Column<int>(type: "integer", nullable: false),
                    name = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_prom", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "skill",
                columns: table => new
                {
                    id_skill = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    name = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_skill", x => x.id_skill);
                });

            migrationBuilder.CreateTable(
                name: "group",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    name = table.Column<string>(type: "text", nullable: false),
                    is_apprenticeship = table.Column<bool>(type: "boolean", nullable: false),
                    id_groupe_parent = table.Column<int>(type: "integer", nullable: true),
                    id_prom = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_group", x => x.id);
                    table.ForeignKey(
                        name: "FK_group_group_id_groupe_parent",
                        column: x => x.id_groupe_parent,
                        principalTable: "group",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "FK_group_prom_id_prom",
                        column: x => x.id_prom,
                        principalTable: "prom",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "sae",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    name = table.Column<string>(type: "text", nullable: false),
                    description = table.Column<string>(type: "text", nullable: false),
                    id_prom = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_sae", x => x.id);
                    table.ForeignKey(
                        name: "FK_sae_prom_id_prom",
                        column: x => x.id_prom,
                        principalTable: "prom",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "character_skill",
                columns: table => new
                {
                    id_character_skill = table.Column<Guid>(type: "uuid", nullable: false),
                    id_character = table.Column<Guid>(type: "uuid", nullable: false),
                    id_skill = table.Column<int>(type: "integer", nullable: false),
                    confidence_level = table.Column<int>(type: "integer", maxLength: 5, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_character_skill", x => x.id_character_skill);
                    table.ForeignKey(
                        name: "FK_character_skill_character_id_character",
                        column: x => x.id_character,
                        principalTable: "character",
                        principalColumn: "id_character",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_character_skill_skill_id_skill",
                        column: x => x.id_skill,
                        principalTable: "skill",
                        principalColumn: "id_skill",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_user_id_groupe",
                table: "user",
                column: "id_groupe");

            migrationBuilder.CreateIndex(
                name: "IX_subject_Saeid",
                table: "subject",
                column: "Saeid");

            migrationBuilder.CreateIndex(
                name: "IX_character_id_user",
                table: "character",
                column: "id_user");

            migrationBuilder.CreateIndex(
                name: "IX_character_skill_id_character",
                table: "character_skill",
                column: "id_character");

            migrationBuilder.CreateIndex(
                name: "IX_character_skill_id_skill",
                table: "character_skill",
                column: "id_skill");

            migrationBuilder.CreateIndex(
                name: "IX_group_id_groupe_parent",
                table: "group",
                column: "id_groupe_parent");

            migrationBuilder.CreateIndex(
                name: "IX_group_id_prom",
                table: "group",
                column: "id_prom");

            migrationBuilder.CreateIndex(
                name: "IX_sae_id_prom",
                table: "sae",
                column: "id_prom");

            migrationBuilder.AddForeignKey(
                name: "FK_subject_sae_Saeid",
                table: "subject",
                column: "Saeid",
                principalTable: "sae",
                principalColumn: "id");

            migrationBuilder.AddForeignKey(
                name: "FK_user_group_id_groupe",
                table: "user",
                column: "id_groupe",
                principalTable: "group",
                principalColumn: "id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_subject_sae_Saeid",
                table: "subject");

            migrationBuilder.DropForeignKey(
                name: "FK_user_group_id_groupe",
                table: "user");

            migrationBuilder.DropTable(
                name: "character_skill");

            migrationBuilder.DropTable(
                name: "group");

            migrationBuilder.DropTable(
                name: "sae");

            migrationBuilder.DropTable(
                name: "character");

            migrationBuilder.DropTable(
                name: "skill");

            migrationBuilder.DropTable(
                name: "prom");

            migrationBuilder.DropIndex(
                name: "IX_user_id_groupe",
                table: "user");

            migrationBuilder.DropIndex(
                name: "IX_subject_Saeid",
                table: "subject");

            migrationBuilder.DropColumn(
                name: "id_groupe",
                table: "user");

            migrationBuilder.DropColumn(
                name: "Saeid",
                table: "subject");
        }
    }
}
