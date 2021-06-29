using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace AspirantDatabase.Migrations
{
    public partial class RemoveFiles : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Document",
                table: "Abstracts");

            migrationBuilder.DropColumn(
                name: "DocumentEdit",
                table: "Abstracts");

            migrationBuilder.DropColumn(
                name: "FileName",
                table: "Abstracts");

            migrationBuilder.DropColumn(
                name: "FileNameEdit",
                table: "Abstracts");

            migrationBuilder.DropColumn(
                name: "FileTime",
                table: "Abstracts");

            migrationBuilder.DropColumn(
                name: "FileTimeEdit",
                table: "Abstracts");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<byte[]>(
                name: "Document",
                table: "Abstracts",
                type: "varbinary(max)",
                nullable: true);

            migrationBuilder.AddColumn<byte[]>(
                name: "DocumentEdit",
                table: "Abstracts",
                type: "varbinary(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "FileName",
                table: "Abstracts",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "FileNameEdit",
                table: "Abstracts",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "FileTime",
                table: "Abstracts",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "FileTimeEdit",
                table: "Abstracts",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }
    }
}
