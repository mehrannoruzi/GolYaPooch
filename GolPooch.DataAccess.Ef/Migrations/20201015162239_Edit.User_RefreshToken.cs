using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace GolPooch.DataAccess.Ef.Migrations
{
    public partial class EditUser_RefreshToken : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "RefreshTokenExpireTime",
                schema: "Base",
                table: "User",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AlterColumn<string>(
                name: "RefreshToken",
                schema: "Base",
                table: "User",
                type: "char(32)",
                maxLength: 32,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "char(32)",
                oldMaxLength: 32);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "RefreshTokenExpireTime",
                schema: "Base",
                table: "User",
                type: "datetime2",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "RefreshToken",
                schema: "Base",
                table: "User",
                type: "char(32)",
                maxLength: 32,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "char(32)",
                oldMaxLength: 32,
                oldNullable: true);
        }
    }
}
