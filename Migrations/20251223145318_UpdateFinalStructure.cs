using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EBOOK.Migrations
{
    /// <inheritdoc />
    public partial class UpdateFinalStructure : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Status",
                table: "Presences",
                newName: "IsPresent");

            migrationBuilder.AddColumn<bool>(
                name: "IsDelegate",
                table: "Students",
                type: "INTEGER",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "Date",
                table: "Presences",
                type: "TEXT",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "Remarks",
                table: "Presences",
                type: "TEXT",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "Students",
                keyColumn: "Id",
                keyValue: 1,
                column: "IsDelegate",
                value: false);

            migrationBuilder.UpdateData(
                table: "Students",
                keyColumn: "Id",
                keyValue: 2,
                column: "IsDelegate",
                value: false);

            migrationBuilder.UpdateData(
                table: "Students",
                keyColumn: "Id",
                keyValue: 3,
                column: "IsDelegate",
                value: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsDelegate",
                table: "Students");

            migrationBuilder.DropColumn(
                name: "Date",
                table: "Presences");

            migrationBuilder.DropColumn(
                name: "Remarks",
                table: "Presences");

            migrationBuilder.RenameColumn(
                name: "IsPresent",
                table: "Presences",
                newName: "Status");
        }
    }
}
