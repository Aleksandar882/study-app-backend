using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace StudyApp.Migrations
{
    /// <inheritdoc />
    public partial class AddUserProgressSeed : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "UserProgresses",
                columns: new[] { "Id", "Completed", "CompletedAt", "LessonId", "UserId" },
                values: new object[,]
                {
                    { 1, true, new DateTime(2025, 11, 7, 0, 0, 0, 0, DateTimeKind.Utc), 1, "john123" },
                    { 2, false, null, 2, "john123" },
                    { 3, true, new DateTime(2025, 11, 7, 0, 0, 0, 0, DateTimeKind.Utc), 3, "jane456" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "UserProgresses",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "UserProgresses",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "UserProgresses",
                keyColumn: "Id",
                keyValue: 3);
        }
    }
}
