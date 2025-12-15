using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace EBOOK.Migrations
{
    /// <inheritdoc />
    public partial class AddCoursesSeed : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Courses",
                keyColumn: "Id",
                keyValue: 2,
                column: "TeacherId",
                value: 1);

            migrationBuilder.InsertData(
                table: "Courses",
                columns: new[] { "Id", "ClassroomId", "Description", "TeacherId", "Title" },
                values: new object[,]
                {
                    { 3, 1, "Algorithmique et programmation C#.", 1, "Informatique" },
                    { 4, 1, "Cellules, ADN et génétique.", 1, "Biologie" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Courses",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Courses",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.UpdateData(
                table: "Courses",
                keyColumn: "Id",
                keyValue: 2,
                column: "TeacherId",
                value: 2);
        }
    }
}
