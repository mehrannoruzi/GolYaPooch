using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace GolPooch.DataAccess.Ef.Migrations
{
    public partial class AddUserDeviceLog : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Type",
                schema: "Product",
                table: "Product",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<long>(
                name: "MobileNumber",
                schema: "Base",
                table: "VerificationCode",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AlterColumn<string>(
                name: "PushId",
                schema: "Base",
                table: "User",
                type: "varchar(150)",
                maxLength: 150,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(150)",
                oldMaxLength: 150);

            migrationBuilder.AlterColumn<string>(
                name: "ProfileImgUrl",
                schema: "Base",
                table: "User",
                type: "varchar(250)",
                maxLength: 250,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(250)",
                oldMaxLength: 250);

            migrationBuilder.AlterColumn<string>(
                name: "LastName",
                schema: "Base",
                table: "User",
                maxLength: 30,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(30)",
                oldMaxLength: 30);

            migrationBuilder.AlterColumn<string>(
                name: "FirstName",
                schema: "Base",
                table: "User",
                maxLength: 25,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(25)",
                oldMaxLength: 25);

            migrationBuilder.AlterColumn<string>(
                name: "Email",
                schema: "Base",
                table: "User",
                type: "varchar(150)",
                maxLength: 150,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(150)",
                oldMaxLength: 150);

            migrationBuilder.AddColumn<bool>(
                name: "IsRead",
                schema: "Base",
                table: "Ticket",
                nullable: false,
                defaultValue: false);

            migrationBuilder.CreateTable(
                name: "UserDeviceLog",
                schema: "Base",
                columns: table => new
                {
                    UserDeviceLogId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MobileNumber = table.Column<long>(nullable: false),
                    IsMobile = table.Column<bool>(nullable: false),
                    InsertDateMi = table.Column<DateTime>(nullable: false),
                    InsertDateSh = table.Column<string>(type: "char(10)", maxLength: 10, nullable: true),
                    IP = table.Column<string>(type: "varchar(20)", maxLength: 20, nullable: true),
                    Os = table.Column<string>(type: "varchar(20)", maxLength: 20, nullable: true),
                    Device = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: true),
                    Application = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserDeviceLog", x => x.UserDeviceLogId);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "UserDeviceLog",
                schema: "Base");

            migrationBuilder.DropColumn(
                name: "Type",
                schema: "Product",
                table: "Product");

            migrationBuilder.DropColumn(
                name: "MobileNumber",
                schema: "Base",
                table: "VerificationCode");

            migrationBuilder.DropColumn(
                name: "IsRead",
                schema: "Base",
                table: "Ticket");

            migrationBuilder.AlterColumn<string>(
                name: "PushId",
                schema: "Base",
                table: "User",
                type: "varchar(150)",
                maxLength: 150,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(150)",
                oldMaxLength: 150,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "ProfileImgUrl",
                schema: "Base",
                table: "User",
                type: "varchar(250)",
                maxLength: 250,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(250)",
                oldMaxLength: 250,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "LastName",
                schema: "Base",
                table: "User",
                type: "nvarchar(30)",
                maxLength: 30,
                nullable: false,
                oldClrType: typeof(string),
                oldMaxLength: 30,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "FirstName",
                schema: "Base",
                table: "User",
                type: "nvarchar(25)",
                maxLength: 25,
                nullable: false,
                oldClrType: typeof(string),
                oldMaxLength: 25,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Email",
                schema: "Base",
                table: "User",
                type: "varchar(150)",
                maxLength: 150,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(150)",
                oldMaxLength: 150,
                oldNullable: true);
        }
    }
}
