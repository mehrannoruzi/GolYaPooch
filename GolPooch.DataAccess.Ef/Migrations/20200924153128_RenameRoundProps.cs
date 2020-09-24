using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace GolPooch.DataAccess.Ef.Migrations
{
    public partial class RenameRoundProps : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "EndDateMi",
                schema: "Draw",
                table: "Round");

            migrationBuilder.DropColumn(
                name: "StartDateMi",
                schema: "Draw",
                table: "Round");

            migrationBuilder.RenameColumn(
                name: "StartDateSh",
                schema: "Draw",
                table: "Round",
                newName: "OpenDateSh");

            migrationBuilder.RenameColumn(
                name: "EndDateSh",
                schema: "Draw",
                table: "Round",
                newName: "CloseDateSh");

            migrationBuilder.AddColumn<DateTime>(
                name: "CloseDateMi",
                schema: "Draw",
                table: "Round",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "OpenDateMi",
                schema: "Draw",
                table: "Round",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CloseDateMi",
                schema: "Draw",
                table: "Round");

            migrationBuilder.DropColumn(
                name: "OpenDateMi",
                schema: "Draw",
                table: "Round");

            migrationBuilder.RenameColumn(
                name: "OpenDateSh",
                schema: "Draw",
                table: "Round",
                newName: "StartDateSh");

            migrationBuilder.RenameColumn(
                name: "CloseDateSh",
                schema: "Draw",
                table: "Round",
                newName: "EndDateSh");

            migrationBuilder.AddColumn<DateTime>(
                name: "EndDateMi",
                schema: "Draw",
                table: "Round",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "StartDateMi",
                schema: "Draw",
                table: "Round",
                type: "datetime2",
                nullable: true);
        }
    }
}
