using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace GolPooch.DataAccess.Ef.Migrations
{
    public partial class AddExpireDate2Purchase : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "ExpireDateMi",
                schema: "Product",
                table: "Purchase",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "ExpireDateSh",
                schema: "Product",
                table: "Purchase",
                type: "char(10)",
                maxLength: 10,
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ExpireDateMi",
                schema: "Product",
                table: "Purchase");

            migrationBuilder.DropColumn(
                name: "ExpireDateSh",
                schema: "Product",
                table: "Purchase");
        }
    }
}
