using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace GolPooch.DataAccess.Ef.Migrations
{
    public partial class AddDrowDate2Round : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ModifyDateMi",
                schema: "Draw",
                table: "Round");

            migrationBuilder.RenameColumn(
                name: "ModifyDateSh",
                schema: "Draw",
                table: "Round",
                newName: "DrawDateSh");

            migrationBuilder.AddColumn<DateTime>(
                name: "DrawDateMi",
                schema: "Draw",
                table: "Round",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DrawDateMi",
                schema: "Draw",
                table: "Round");

            migrationBuilder.RenameColumn(
                name: "DrawDateSh",
                schema: "Draw",
                table: "Round",
                newName: "ModifyDateSh");

            migrationBuilder.AddColumn<DateTime>(
                name: "ModifyDateMi",
                schema: "Draw",
                table: "Round",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }
    }
}
