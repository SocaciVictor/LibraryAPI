using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LibraryAPI.Migrations
{
    /// <inheritdoc />
    public partial class UpdateUserDb : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Loans_UserDb_UserId",
                table: "Loans");

            migrationBuilder.DropPrimaryKey(
                name: "PK_UserDb",
                table: "UserDb");

            migrationBuilder.RenameTable(
                name: "UserDb",
                newName: "Users");

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "Users",
                type: "tinyint(1)",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Users",
                table: "Users",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Loans_Users_UserId",
                table: "Loans",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Loans_Users_UserId",
                table: "Loans");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Users",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "Users");

            migrationBuilder.RenameTable(
                name: "Users",
                newName: "UserDb");

            migrationBuilder.AddPrimaryKey(
                name: "PK_UserDb",
                table: "UserDb",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Loans_UserDb_UserId",
                table: "Loans",
                column: "UserId",
                principalTable: "UserDb",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
