using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace backend.Migrations
{
    /// <inheritdoc />
    public partial class AddUserFKey : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_user_role_user_id_role",
                table: "user");

            migrationBuilder.RenameColumn(
                name: "id_role",
                table: "user",
                newName: "role_id");

            migrationBuilder.RenameIndex(
                name: "IX_user_id_role",
                table: "user",
                newName: "IX_user_role_id");

            migrationBuilder.AddForeignKey(
                name: "FK_user_role_user_role_id",
                table: "user",
                column: "role_id",
                principalTable: "role_user",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_user_role_user_role_id",
                table: "user");

            migrationBuilder.RenameColumn(
                name: "role_id",
                table: "user",
                newName: "id_role");

            migrationBuilder.RenameIndex(
                name: "IX_user_role_id",
                table: "user",
                newName: "IX_user_id_role");

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
