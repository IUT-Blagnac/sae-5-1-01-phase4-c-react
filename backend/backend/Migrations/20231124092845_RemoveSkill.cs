using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace backend.Migrations
{
    /// <inheritdoc />
    public partial class RemoveSkill : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "character_skill");

            migrationBuilder.DropTable(
                name: "skill");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "skill",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    name = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_skill", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "character_skill",
                columns: table => new
                {
                    id_character = table.Column<Guid>(type: "uuid", nullable: false),
                    id_skill = table.Column<int>(type: "integer", nullable: false),
                    confidence_level = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_character_skill", x => new { x.id_character, x.id_skill });
                    table.ForeignKey(
                        name: "FK_character_skill_character_id_character",
                        column: x => x.id_character,
                        principalTable: "character",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_character_skill_skill_id_skill",
                        column: x => x.id_skill,
                        principalTable: "skill",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_character_skill_id_skill",
                table: "character_skill",
                column: "id_skill");
        }
    }
}
