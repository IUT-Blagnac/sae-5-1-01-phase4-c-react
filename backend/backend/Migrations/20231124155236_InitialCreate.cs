using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace backend.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "category",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    name = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_category", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "group",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    name = table.Column<string>(type: "text", nullable: false),
                    is_apprenticeship = table.Column<bool>(type: "boolean", nullable: false),
                    id_group_parent = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_group", x => x.id);
                    table.ForeignKey(
                        name: "FK_group_group_id_group_parent",
                        column: x => x.id_group_parent,
                        principalTable: "group",
                        principalColumn: "id");
                });

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

            migrationBuilder.CreateTable(
                name: "sae",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    name = table.Column<string>(type: "text", nullable: false),
                    description = table.Column<string>(type: "text", nullable: false),
                    min_student_per_group = table.Column<int>(type: "integer", nullable: true),
                    max_student_per_group = table.Column<int>(type: "integer", nullable: true),
                    min_group_per_subject = table.Column<int>(type: "integer", nullable: true),
                    max_group_per_subject = table.Column<int>(type: "integer", nullable: true),
                    state = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_sae", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "skill",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    name = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_skill", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "team",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    name = table.Column<string>(type: "text", nullable: false),
                    color = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_team", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "user",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    email = table.Column<string>(type: "text", nullable: false),
                    password = table.Column<string>(type: "text", nullable: false),
                    first_name = table.Column<string>(type: "text", nullable: false),
                    last_name = table.Column<string>(type: "text", nullable: false),
                    id_role = table.Column<int>(type: "integer", nullable: false),
                    id_group = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_user", x => x.id);
                    table.ForeignKey(
                        name: "FK_user_group_id_group",
                        column: x => x.id_group,
                        principalTable: "group",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "FK_user_role_id_role",
                        column: x => x.id_role,
                        principalTable: "role",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "sae_group",
                columns: table => new
                {
                    id_sae = table.Column<Guid>(type: "uuid", nullable: false),
                    id_group = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_sae_group", x => new { x.id_sae, x.id_group });
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
                name: "subject",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    name = table.Column<string>(type: "text", nullable: false),
                    description = table.Column<string>(type: "text", nullable: false),
                    id_sae = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_subject", x => x.id);
                    table.ForeignKey(
                        name: "FK_subject_sae_id_sae",
                        column: x => x.id_sae,
                        principalTable: "sae",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "challenge",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    name = table.Column<string>(type: "text", nullable: false),
                    description = table.Column<string>(type: "text", nullable: false),
                    creator_team_id = table.Column<Guid>(type: "uuid", nullable: false),
                    target_team_id = table.Column<Guid>(type: "uuid", nullable: false),
                    completed = table.Column<bool>(type: "boolean", nullable: false)
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

            migrationBuilder.CreateTable(
                name: "character",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    name = table.Column<string>(type: "text", nullable: false),
                    id_user = table.Column<Guid>(type: "uuid", nullable: false),
                    id_sae = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_character", x => x.id);
                    table.ForeignKey(
                        name: "FK_character_sae_id_sae",
                        column: x => x.id_sae,
                        principalTable: "sae",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_character_user_id_user",
                        column: x => x.id_user,
                        principalTable: "user",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "sae_coach",
                columns: table => new
                {
                    id_sae = table.Column<Guid>(type: "uuid", nullable: false),
                    id_coach = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_sae_coach", x => new { x.id_sae, x.id_coach });
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
                name: "user_team",
                columns: table => new
                {
                    id_user = table.Column<Guid>(type: "uuid", nullable: false),
                    id_team = table.Column<Guid>(type: "uuid", nullable: false),
                    role = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_user_team", x => new { x.id_user, x.id_team });
                    table.ForeignKey(
                        name: "FK_user_team_team_id_team",
                        column: x => x.id_team,
                        principalTable: "team",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_user_team_user_id_user",
                        column: x => x.id_user,
                        principalTable: "user",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "subject_category",
                columns: table => new
                {
                    id_subject = table.Column<Guid>(type: "uuid", nullable: false),
                    id_category = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_subject_category", x => new { x.id_subject, x.id_category });
                    table.ForeignKey(
                        name: "FK_subject_category_category_id_category",
                        column: x => x.id_category,
                        principalTable: "category",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_subject_category_subject_id_subject",
                        column: x => x.id_subject,
                        principalTable: "subject",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "team_subject",
                columns: table => new
                {
                    id_team = table.Column<Guid>(type: "uuid", nullable: false),
                    id_subject = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_team_subject", x => new { x.id_team, x.id_subject });
                    table.ForeignKey(
                        name: "FK_team_subject_subject_id_subject",
                        column: x => x.id_subject,
                        principalTable: "subject",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_team_subject_team_id_team",
                        column: x => x.id_team,
                        principalTable: "team",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "team_wish",
                columns: table => new
                {
                    id_team = table.Column<Guid>(type: "uuid", nullable: false),
                    id_subject = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_team_wish", x => new { x.id_team, x.id_subject });
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

            migrationBuilder.CreateTable(
                name: "character_skill",
                columns: table => new
                {
                    id_character = table.Column<Guid>(type: "uuid", nullable: false),
                    id_skill = table.Column<Guid>(type: "uuid", nullable: false),
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
                name: "IX_challenge_creator_team_id",
                table: "challenge",
                column: "creator_team_id");

            migrationBuilder.CreateIndex(
                name: "IX_challenge_target_team_id",
                table: "challenge",
                column: "target_team_id");

            migrationBuilder.CreateIndex(
                name: "IX_character_id_sae",
                table: "character",
                column: "id_sae");

            migrationBuilder.CreateIndex(
                name: "IX_character_id_user",
                table: "character",
                column: "id_user");

            migrationBuilder.CreateIndex(
                name: "IX_character_skill_id_skill",
                table: "character_skill",
                column: "id_skill");

            migrationBuilder.CreateIndex(
                name: "IX_group_id_group_parent",
                table: "group",
                column: "id_group_parent");

            migrationBuilder.CreateIndex(
                name: "IX_sae_coach_id_coach",
                table: "sae_coach",
                column: "id_coach");

            migrationBuilder.CreateIndex(
                name: "IX_sae_group_id_group",
                table: "sae_group",
                column: "id_group");

            migrationBuilder.CreateIndex(
                name: "IX_subject_id_sae",
                table: "subject",
                column: "id_sae");

            migrationBuilder.CreateIndex(
                name: "IX_subject_category_id_category",
                table: "subject_category",
                column: "id_category");

            migrationBuilder.CreateIndex(
                name: "IX_team_subject_id_subject",
                table: "team_subject",
                column: "id_subject");

            migrationBuilder.CreateIndex(
                name: "IX_team_wish_id_subject",
                table: "team_wish",
                column: "id_subject");

            migrationBuilder.CreateIndex(
                name: "IX_user_id_group",
                table: "user",
                column: "id_group");

            migrationBuilder.CreateIndex(
                name: "IX_user_id_role",
                table: "user",
                column: "id_role");

            migrationBuilder.CreateIndex(
                name: "IX_user_team_id_team",
                table: "user_team",
                column: "id_team");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "challenge");

            migrationBuilder.DropTable(
                name: "character_skill");

            migrationBuilder.DropTable(
                name: "sae_coach");

            migrationBuilder.DropTable(
                name: "sae_group");

            migrationBuilder.DropTable(
                name: "subject_category");

            migrationBuilder.DropTable(
                name: "team_subject");

            migrationBuilder.DropTable(
                name: "team_wish");

            migrationBuilder.DropTable(
                name: "user_team");

            migrationBuilder.DropTable(
                name: "character");

            migrationBuilder.DropTable(
                name: "skill");

            migrationBuilder.DropTable(
                name: "category");

            migrationBuilder.DropTable(
                name: "subject");

            migrationBuilder.DropTable(
                name: "team");

            migrationBuilder.DropTable(
                name: "user");

            migrationBuilder.DropTable(
                name: "sae");

            migrationBuilder.DropTable(
                name: "group");

            migrationBuilder.DropTable(
                name: "role");
        }
    }
}
