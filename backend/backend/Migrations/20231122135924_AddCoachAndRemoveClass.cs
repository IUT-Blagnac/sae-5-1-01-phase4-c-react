using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace backend.Migrations
{
    /// <inheritdoc />
    public partial class AddCoachAndRemoveClass : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_group_group_id_groupe_parent",
                table: "group");

            migrationBuilder.DropForeignKey(
                name: "FK_group_prom_id_prom",
                table: "group");

            migrationBuilder.DropForeignKey(
                name: "FK_sae_prom_id_prom",
                table: "sae");

            migrationBuilder.DropForeignKey(
                name: "FK_subject_category_category_id",
                table: "subject");

            migrationBuilder.DropForeignKey(
                name: "FK_subject_sae_Saeid",
                table: "subject");

            migrationBuilder.DropForeignKey(
                name: "FK_user_group_id_groupe",
                table: "user");

            migrationBuilder.DropForeignKey(
                name: "FK_user_role_user_role_id",
                table: "user");

            migrationBuilder.DropForeignKey(
                name: "FK_user_equipe_team_team_id",
                table: "user_equipe");

            migrationBuilder.DropForeignKey(
                name: "FK_user_equipe_user_user_id",
                table: "user_equipe");

            migrationBuilder.DropTable(
                name: "prom");

            migrationBuilder.DropTable(
                name: "wish");

            migrationBuilder.DropIndex(
                name: "IX_subject_Saeid",
                table: "subject");

            migrationBuilder.DropIndex(
                name: "IX_sae_id_prom",
                table: "sae");

            migrationBuilder.DropIndex(
                name: "IX_group_id_prom",
                table: "group");

            migrationBuilder.DropPrimaryKey(
                name: "PK_user_equipe",
                table: "user_equipe");

            migrationBuilder.DropColumn(
                name: "Saeid",
                table: "subject");

            migrationBuilder.DropColumn(
                name: "id_prom",
                table: "sae");

            migrationBuilder.DropColumn(
                name: "id_prom",
                table: "group");

            migrationBuilder.RenameTable(
                name: "user_equipe",
                newName: "user_team");

            migrationBuilder.RenameColumn(
                name: "role_id",
                table: "user",
                newName: "id_role");

            migrationBuilder.RenameColumn(
                name: "id_groupe",
                table: "user",
                newName: "id_group");

            migrationBuilder.RenameIndex(
                name: "IX_user_role_id",
                table: "user",
                newName: "IX_user_id_role");

            migrationBuilder.RenameIndex(
                name: "IX_user_id_groupe",
                table: "user",
                newName: "IX_user_id_group");

            migrationBuilder.RenameColumn(
                name: "category_id",
                table: "subject",
                newName: "id_category");

            migrationBuilder.RenameIndex(
                name: "IX_subject_category_id",
                table: "subject",
                newName: "IX_subject_id_category");

            migrationBuilder.RenameColumn(
                name: "id_skill",
                table: "skill",
                newName: "id");

            migrationBuilder.RenameColumn(
                name: "id_groupe_parent",
                table: "group",
                newName: "id_group_parent");

            migrationBuilder.RenameIndex(
                name: "IX_group_id_groupe_parent",
                table: "group",
                newName: "IX_group_id_group_parent");

            migrationBuilder.RenameColumn(
                name: "id_character_skill",
                table: "character_skill",
                newName: "id");

            migrationBuilder.RenameColumn(
                name: "id_character",
                table: "character",
                newName: "id");

            migrationBuilder.RenameColumn(
                name: "team_id",
                table: "user_team",
                newName: "id_team");

            migrationBuilder.RenameColumn(
                name: "user_id",
                table: "user_team",
                newName: "id_user");

            migrationBuilder.RenameIndex(
                name: "IX_user_equipe_team_id",
                table: "user_team",
                newName: "IX_user_team_id_team");

            migrationBuilder.AddColumn<int>(
                name: "id_sae",
                table: "subject",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "max_group_per_subject",
                table: "sae",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "max_student_per_group",
                table: "sae",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "min_group_per_subject",
                table: "sae",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "min_student_per_group",
                table: "sae",
                type: "integer",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_user_team",
                table: "user_team",
                columns: new[] { "id_user", "id_team" });

            migrationBuilder.CreateTable(
                name: "sae_coach",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    id_sae = table.Column<int>(type: "integer", nullable: false),
                    id_coach = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_sae_coach", x => x.id);
                    table.ForeignKey(
                        name: "FK_sae_coach_sae_id_sae",
                        column: x => x.id_sae,
                        principalTable: "sae",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_sae_coach_user_id_coach",
                        column: x => x.id_coach,
                        principalTable: "user",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "sae_group",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    id_sae = table.Column<int>(type: "integer", nullable: false),
                    id_group = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_sae_group", x => x.id);
                    table.ForeignKey(
                        name: "FK_sae_group_group_id_group",
                        column: x => x.id_group,
                        principalTable: "group",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_sae_group_sae_id_sae",
                        column: x => x.id_sae,
                        principalTable: "sae",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "team_wish",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    id_team = table.Column<Guid>(type: "uuid", nullable: false),
                    id_subject = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_team_wish", x => x.id);
                    table.ForeignKey(
                        name: "FK_team_wish_subject_id_subject",
                        column: x => x.id_subject,
                        principalTable: "subject",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_team_wish_team_id_team",
                        column: x => x.id_team,
                        principalTable: "team",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_subject_id_sae",
                table: "subject",
                column: "id_sae");

            migrationBuilder.CreateIndex(
                name: "IX_sae_coach_id_coach",
                table: "sae_coach",
                column: "id_coach");

            migrationBuilder.CreateIndex(
                name: "IX_sae_coach_id_sae",
                table: "sae_coach",
                column: "id_sae");

            migrationBuilder.CreateIndex(
                name: "IX_sae_group_id_group",
                table: "sae_group",
                column: "id_group");

            migrationBuilder.CreateIndex(
                name: "IX_sae_group_id_sae",
                table: "sae_group",
                column: "id_sae");

            migrationBuilder.CreateIndex(
                name: "IX_team_wish_id_subject",
                table: "team_wish",
                column: "id_subject");

            migrationBuilder.CreateIndex(
                name: "IX_team_wish_id_team",
                table: "team_wish",
                column: "id_team");

            migrationBuilder.AddForeignKey(
                name: "FK_group_group_id_group_parent",
                table: "group",
                column: "id_group_parent",
                principalTable: "group",
                principalColumn: "id");

            migrationBuilder.AddForeignKey(
                name: "FK_subject_category_id_category",
                table: "subject",
                column: "id_category",
                principalTable: "category",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_subject_sae_id_sae",
                table: "subject",
                column: "id_sae",
                principalTable: "sae",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_user_group_id_group",
                table: "user",
                column: "id_group",
                principalTable: "group",
                principalColumn: "id");

            migrationBuilder.AddForeignKey(
                name: "FK_user_role_user_id_role",
                table: "user",
                column: "id_role",
                principalTable: "role_user",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_user_team_team_id_team",
                table: "user_team",
                column: "id_team",
                principalTable: "team",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_user_team_user_id_user",
                table: "user_team",
                column: "id_user",
                principalTable: "user",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_group_group_id_group_parent",
                table: "group");

            migrationBuilder.DropForeignKey(
                name: "FK_subject_category_id_category",
                table: "subject");

            migrationBuilder.DropForeignKey(
                name: "FK_subject_sae_id_sae",
                table: "subject");

            migrationBuilder.DropForeignKey(
                name: "FK_user_group_id_group",
                table: "user");

            migrationBuilder.DropForeignKey(
                name: "FK_user_role_user_id_role",
                table: "user");

            migrationBuilder.DropForeignKey(
                name: "FK_user_team_team_id_team",
                table: "user_team");

            migrationBuilder.DropForeignKey(
                name: "FK_user_team_user_id_user",
                table: "user_team");

            migrationBuilder.DropTable(
                name: "sae_coach");

            migrationBuilder.DropTable(
                name: "sae_group");

            migrationBuilder.DropTable(
                name: "team_wish");

            migrationBuilder.DropIndex(
                name: "IX_subject_id_sae",
                table: "subject");

            migrationBuilder.DropPrimaryKey(
                name: "PK_user_team",
                table: "user_team");

            migrationBuilder.DropColumn(
                name: "id_sae",
                table: "subject");

            migrationBuilder.DropColumn(
                name: "max_group_per_subject",
                table: "sae");

            migrationBuilder.DropColumn(
                name: "max_student_per_group",
                table: "sae");

            migrationBuilder.DropColumn(
                name: "min_group_per_subject",
                table: "sae");

            migrationBuilder.DropColumn(
                name: "min_student_per_group",
                table: "sae");

            migrationBuilder.RenameTable(
                name: "user_team",
                newName: "user_equipe");

            migrationBuilder.RenameColumn(
                name: "id_role",
                table: "user",
                newName: "role_id");

            migrationBuilder.RenameColumn(
                name: "id_group",
                table: "user",
                newName: "id_groupe");

            migrationBuilder.RenameIndex(
                name: "IX_user_id_role",
                table: "user",
                newName: "IX_user_role_id");

            migrationBuilder.RenameIndex(
                name: "IX_user_id_group",
                table: "user",
                newName: "IX_user_id_groupe");

            migrationBuilder.RenameColumn(
                name: "id_category",
                table: "subject",
                newName: "category_id");

            migrationBuilder.RenameIndex(
                name: "IX_subject_id_category",
                table: "subject",
                newName: "IX_subject_category_id");

            migrationBuilder.RenameColumn(
                name: "id",
                table: "skill",
                newName: "id_skill");

            migrationBuilder.RenameColumn(
                name: "id_group_parent",
                table: "group",
                newName: "id_groupe_parent");

            migrationBuilder.RenameIndex(
                name: "IX_group_id_group_parent",
                table: "group",
                newName: "IX_group_id_groupe_parent");

            migrationBuilder.RenameColumn(
                name: "id",
                table: "character_skill",
                newName: "id_character_skill");

            migrationBuilder.RenameColumn(
                name: "id",
                table: "character",
                newName: "id_character");

            migrationBuilder.RenameColumn(
                name: "id_team",
                table: "user_equipe",
                newName: "team_id");

            migrationBuilder.RenameColumn(
                name: "id_user",
                table: "user_equipe",
                newName: "user_id");

            migrationBuilder.RenameIndex(
                name: "IX_user_team_id_team",
                table: "user_equipe",
                newName: "IX_user_equipe_team_id");

            migrationBuilder.AddColumn<int>(
                name: "Saeid",
                table: "subject",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "id_prom",
                table: "sae",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "id_prom",
                table: "group",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddPrimaryKey(
                name: "PK_user_equipe",
                table: "user_equipe",
                columns: new[] { "user_id", "team_id" });

            migrationBuilder.CreateTable(
                name: "prom",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    name = table.Column<string>(type: "text", nullable: false),
                    year = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_prom", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "wish",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    subject_id = table.Column<Guid>(type: "uuid", nullable: false),
                    team_id = table.Column<Guid>(type: "uuid", nullable: false)
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
                name: "IX_subject_Saeid",
                table: "subject",
                column: "Saeid");

            migrationBuilder.CreateIndex(
                name: "IX_sae_id_prom",
                table: "sae",
                column: "id_prom");

            migrationBuilder.CreateIndex(
                name: "IX_group_id_prom",
                table: "group",
                column: "id_prom");

            migrationBuilder.CreateIndex(
                name: "IX_wish_subject_id",
                table: "wish",
                column: "subject_id");

            migrationBuilder.CreateIndex(
                name: "IX_wish_team_id",
                table: "wish",
                column: "team_id");

            migrationBuilder.AddForeignKey(
                name: "FK_group_group_id_groupe_parent",
                table: "group",
                column: "id_groupe_parent",
                principalTable: "group",
                principalColumn: "id");

            migrationBuilder.AddForeignKey(
                name: "FK_group_prom_id_prom",
                table: "group",
                column: "id_prom",
                principalTable: "prom",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_sae_prom_id_prom",
                table: "sae",
                column: "id_prom",
                principalTable: "prom",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_subject_category_category_id",
                table: "subject",
                column: "category_id",
                principalTable: "category",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

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

            migrationBuilder.AddForeignKey(
                name: "FK_user_role_user_role_id",
                table: "user",
                column: "role_id",
                principalTable: "role_user",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_user_equipe_team_team_id",
                table: "user_equipe",
                column: "team_id",
                principalTable: "team",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_user_equipe_user_user_id",
                table: "user_equipe",
                column: "user_id",
                principalTable: "user",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
