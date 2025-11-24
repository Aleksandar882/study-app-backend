using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace StudyApp.Migrations
{
    /// <inheritdoc />
    public partial class AddedOrderInLesson : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Order",
                table: "Lessons",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.UpdateData(
                table: "Lessons",
                keyColumn: "Id",
                keyValue: 1,
                column: "Order",
                value: 0);

            migrationBuilder.UpdateData(
                table: "Lessons",
                keyColumn: "Id",
                keyValue: 2,
                column: "Order",
                value: 0);

            migrationBuilder.UpdateData(
                table: "Lessons",
                keyColumn: "Id",
                keyValue: 3,
                column: "Order",
                value: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Order",
                table: "Lessons");
        }
    }
}
