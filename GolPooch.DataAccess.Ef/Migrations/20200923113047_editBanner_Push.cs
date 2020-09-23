using Microsoft.EntityFrameworkCore.Migrations;

namespace GolPooch.DataAccess.Ef.Migrations
{
    public partial class editBanner_Push : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AuthSecretKey",
                schema: "Messaging",
                table: "PushEndpoint");

            migrationBuilder.DropColumn(
                name: "Endpoint",
                schema: "Messaging",
                table: "PushEndpoint");

            migrationBuilder.RenameColumn(
                name: "P256DhSecretKey",
                schema: "Messaging",
                table: "PushEndpoint",
                newName: "PushKey");

            migrationBuilder.RenameIndex(
                name: "IX_PushEndpoint_P256DhSecretKey",
                schema: "Messaging",
                table: "PushEndpoint",
                newName: "IX_PushEndpoint_PushKey");

            migrationBuilder.AlterColumn<string>(
                name: "ImageUrl",
                schema: "Messaging",
                table: "Banner",
                type: "varchar(250)",
                maxLength: 250,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(250)",
                oldMaxLength: 250);

            migrationBuilder.AlterColumn<string>(
                name: "Href",
                schema: "Messaging",
                table: "Banner",
                type: "varchar(250)",
                maxLength: 250,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(250)",
                oldMaxLength: 250);

            migrationBuilder.AlterColumn<string>(
                name: "ActionText",
                schema: "Messaging",
                table: "Banner",
                type: "varchar(30)",
                maxLength: 30,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(30)",
                oldMaxLength: 30);

            migrationBuilder.AddColumn<string>(
                name: "BackColor",
                schema: "Messaging",
                table: "Banner",
                type: "varchar(100)",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "FontColor",
                schema: "Messaging",
                table: "Banner",
                type: "varchar(50)",
                maxLength: 50,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "IconUrl",
                schema: "Messaging",
                table: "Banner",
                type: "varchar(250)",
                maxLength: 250,
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "BackColor",
                schema: "Messaging",
                table: "Banner");

            migrationBuilder.DropColumn(
                name: "FontColor",
                schema: "Messaging",
                table: "Banner");

            migrationBuilder.DropColumn(
                name: "IconUrl",
                schema: "Messaging",
                table: "Banner");

            migrationBuilder.RenameColumn(
                name: "PushKey",
                schema: "Messaging",
                table: "PushEndpoint",
                newName: "P256DhSecretKey");

            migrationBuilder.RenameIndex(
                name: "IX_PushEndpoint_PushKey",
                schema: "Messaging",
                table: "PushEndpoint",
                newName: "IX_PushEndpoint_P256DhSecretKey");

            migrationBuilder.AddColumn<string>(
                name: "AuthSecretKey",
                schema: "Messaging",
                table: "PushEndpoint",
                type: "varchar(1000)",
                maxLength: 1000,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Endpoint",
                schema: "Messaging",
                table: "PushEndpoint",
                type: "varchar(1000)",
                maxLength: 1000,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AlterColumn<string>(
                name: "ImageUrl",
                schema: "Messaging",
                table: "Banner",
                type: "varchar(250)",
                maxLength: 250,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(250)",
                oldMaxLength: 250,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Href",
                schema: "Messaging",
                table: "Banner",
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
                table: "Banner",
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
