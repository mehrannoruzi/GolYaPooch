using Microsoft.EntityFrameworkCore.Migrations;

namespace GolPooch.DataAccess.Ef.Migrations
{
    public partial class EditRoundWinnerUserId : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Banner_Page_PageId",
                schema: "Messaging",
                table: "Banner");

            migrationBuilder.DropForeignKey(
                name: "FK_NotificationDelivery_Notification_NotificationId",
                schema: "Messaging",
                table: "NotificationDelivery");

            migrationBuilder.DropForeignKey(
                name: "FK_NotificationDelivery_User_UserId",
                schema: "Messaging",
                table: "NotificationDelivery");

            migrationBuilder.AlterColumn<int>(
                name: "WinnerUserId",
                schema: "Draw",
                table: "Round",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_Banner_Page_PageId",
                schema: "Messaging",
                table: "Banner",
                column: "PageId",
                principalSchema: "Base",
                principalTable: "Page",
                principalColumn: "PageId",
                onDelete: ReferentialAction.Restrict);

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

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Banner_Page_PageId",
                schema: "Messaging",
                table: "Banner");

            migrationBuilder.DropForeignKey(
                name: "FK_NotificationDelivery_Notification_NotificationId",
                schema: "Messaging",
                table: "NotificationDelivery");

            migrationBuilder.DropForeignKey(
                name: "FK_NotificationDelivery_User_UserId",
                schema: "Messaging",
                table: "NotificationDelivery");

            migrationBuilder.AlterColumn<int>(
                name: "WinnerUserId",
                schema: "Draw",
                table: "Round",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Banner_Page_PageId",
                schema: "Messaging",
                table: "Banner",
                column: "PageId",
                principalSchema: "Base",
                principalTable: "Page",
                principalColumn: "PageId",
                onDelete: ReferentialAction.Cascade);

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
    }
}
