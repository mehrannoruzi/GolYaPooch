using Microsoft.EntityFrameworkCore.Migrations;

namespace GolPooch.DataAccess.Ef.Migrations
{
    public partial class EditTicket : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_NotificationDelivery_Notification_NotificationId",
                schema: "Messaging",
                table: "NotificationDelivery");

            migrationBuilder.DropForeignKey(
                name: "FK_NotificationDelivery_User_UserId",
                schema: "Messaging",
                table: "NotificationDelivery");

            migrationBuilder.DropColumn(
                name: "OsType",
                schema: "Base",
                table: "User");

            migrationBuilder.AlterColumn<string>(
                name: "Answer",
                schema: "Base",
                table: "Ticket",
                maxLength: 1000,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(1000)",
                oldMaxLength: 1000);

            migrationBuilder.AddForeignKey(
                name: "FK_NotificationDelivery_Notification_NotificationId",
                schema: "Messaging",
                table: "NotificationDelivery",
                column: "NotificationId",
                principalSchema: "Messaging",
                principalTable: "Notification",
                principalColumn: "NotificationId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_NotificationDelivery_User_UserId",
                schema: "Messaging",
                table: "NotificationDelivery",
                column: "UserId",
                principalSchema: "Base",
                principalTable: "User",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_NotificationDelivery_Notification_NotificationId",
                schema: "Messaging",
                table: "NotificationDelivery");

            migrationBuilder.DropForeignKey(
                name: "FK_NotificationDelivery_User_UserId",
                schema: "Messaging",
                table: "NotificationDelivery");

            migrationBuilder.AddColumn<int>(
                name: "OsType",
                schema: "Base",
                table: "User",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<string>(
                name: "Answer",
                schema: "Base",
                table: "Ticket",
                type: "nvarchar(1000)",
                maxLength: 1000,
                nullable: false,
                oldClrType: typeof(string),
                oldMaxLength: 1000,
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_NotificationDelivery_Notification_NotificationId",
                schema: "Messaging",
                table: "NotificationDelivery",
                column: "NotificationId",
                principalSchema: "Messaging",
                principalTable: "Notification",
                principalColumn: "NotificationId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_NotificationDelivery_User_UserId",
                schema: "Messaging",
                table: "NotificationDelivery",
                column: "UserId",
                principalSchema: "Base",
                principalTable: "User",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
