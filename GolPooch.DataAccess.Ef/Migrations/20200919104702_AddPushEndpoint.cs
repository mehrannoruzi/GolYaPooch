using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace GolPooch.DataAccess.Ef.Migrations
{
    public partial class AddPushEndpoint : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ImageUrl",
                schema: "Product",
                table: "ProductOffer",
                type: "varchar(250)",
                maxLength: 250,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AlterColumn<string>(
                name: "ImageUrl",
                schema: "Messaging",
                table: "Notification",
                type: "varchar(250)",
                maxLength: 250,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(250)",
                oldMaxLength: 250);

            migrationBuilder.AlterColumn<string>(
                name: "ActionUrl",
                schema: "Messaging",
                table: "Notification",
                type: "varchar(250)",
                maxLength: 250,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(250)",
                oldMaxLength: 250);

            migrationBuilder.AlterColumn<string>(
                name: "ActionText",
                schema: "Messaging",
                table: "Notification",
                type: "varchar(30)",
                maxLength: 30,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(30)",
                oldMaxLength: 30);

            migrationBuilder.AddColumn<string>(
                name: "IconUrl",
                schema: "Messaging",
                table: "Notification",
                type: "varchar(250)",
                maxLength: 250,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "SendResultMessage",
                schema: "Messaging",
                table: "Notification",
                type: "varchar(100)",
                maxLength: 100,
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateTable(
                name: "PushEndpoint",
                schema: "Messaging",
                columns: table => new
                {
                    PushEndpointId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<int>(nullable: true),
                    InsertDateMi = table.Column<DateTime>(nullable: false),
                    InsertDateSh = table.Column<string>(type: "char(10)", maxLength: 10, nullable: true),
                    Endpoint = table.Column<string>(type: "varchar(1000)", maxLength: 1000, nullable: false),
                    AuthSecretKey = table.Column<string>(type: "varchar(1000)", maxLength: 1000, nullable: false),
                    P256DhSecretKey = table.Column<string>(type: "varchar(1000)", maxLength: 1000, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PushEndpoint", x => x.PushEndpointId);
                });

            migrationBuilder.CreateIndex(
                name: "IX_PushEndpoint_P256DhSecretKey",
                schema: "Messaging",
                table: "PushEndpoint",
                column: "P256DhSecretKey",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PushEndpoint",
                schema: "Messaging");

            migrationBuilder.DropColumn(
                name: "ImageUrl",
                schema: "Product",
                table: "ProductOffer");

            migrationBuilder.DropColumn(
                name: "IconUrl",
                schema: "Messaging",
                table: "Notification");

            migrationBuilder.DropColumn(
                name: "SendResultMessage",
                schema: "Messaging",
                table: "Notification");

            migrationBuilder.AlterColumn<string>(
                name: "ImageUrl",
                schema: "Messaging",
                table: "Notification",
                type: "varchar(250)",
                maxLength: 250,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(250)",
                oldMaxLength: 250,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "ActionUrl",
                schema: "Messaging",
                table: "Notification",
                type: "varchar(250)",
                maxLength: 250,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(250)",
                oldMaxLength: 250,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "ActionText",
                schema: "Messaging",
                table: "Notification",
                type: "varchar(30)",
                maxLength: 30,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(30)",
                oldMaxLength: 30,
                oldNullable: true);
        }
    }
}
