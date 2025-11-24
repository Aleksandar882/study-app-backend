using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace StudyApp.Migrations
{
    /// <inheritdoc />
    public partial class AddUserFK : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_UserProgresses_UserId",
                table: "UserProgresses",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_UserProgresses_Users_UserId",
                table: "UserProgresses",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserProgresses_Users_UserId",
                table: "UserProgresses");

            migrationBuilder.DropIndex(
                name: "IX_UserProgresses_UserId",
                table: "UserProgresses");
        }
    }
}
