using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace backend.Migrations
{
    /// <inheritdoc />
    public partial class AddWish : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "subject",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    name = table.Column<string>(type: "text", nullable: false),
                    description = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_subject", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "wish",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    team_id = table.Column<Guid>(type: "uuid", nullable: false),
                    subject_id = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_wish", x => x.id);
                    table.ForeignKey(
                        name: "FK_wish_subject_subject_id",
                        column: x => x.subject_id,
                        principalTable: "subject",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_wish_team_team_id",
                        column: x => x.team_id,
                        principalTable: "team",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_wish_subject_id",
                table: "wish",
                column: "subject_id");

            migrationBuilder.CreateIndex(
                name: "IX_wish_team_id",
                table: "wish",
                column: "team_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "wish");

            migrationBuilder.DropTable(
                name: "subject");
        }
    }
}
