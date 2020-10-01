using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace GolPooch.DataAccess.Ef.Migrations
{
    public partial class AddRoundWinner : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Round_User_WinnerUserId",
                schema: "Draw",
                table: "Round");

            migrationBuilder.DropIndex(
                name: "IX_Round_WinnerUserId",
                schema: "Draw",
                table: "Round");

            migrationBuilder.DropColumn(
                name: "WinnerUserId",
                schema: "Draw",
                table: "Round");

            migrationBuilder.AlterColumn<int>(
                name: "UserId",
                schema: "Messaging",
                table: "PushEndpoint",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddColumn<byte>(
                name: "Priority",
                schema: "Messaging",
                table: "Notification",
                nullable: false,
                defaultValue: (byte)0);

            migrationBuilder.AddColumn<int>(
                name: "UserId",
                schema: "Draw",
                table: "Round",
                nullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "UserId",
                schema: "Draw",
                table: "DrawChance",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.CreateTable(
                name: "RoundWinner",
                schema: "Draw",
                columns: table => new
                {
                    RoundWinnerId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoundId = table.Column<int>(nullable: false),
                    UserId = table.Column<int>(nullable: false),
                    InsertDateMi = table.Column<DateTime>(nullable: false),
                    InsertDateSh = table.Column<string>(type: "char(10)", maxLength: 10, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RoundWinner", x => x.RoundWinnerId);
                    table.ForeignKey(
                        name: "FK_RoundWinner_Round_RoundId",
                        column: x => x.RoundId,
                        principalSchema: "Draw",
                        principalTable: "Round",
                        principalColumn: "RoundId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_RoundWinner_User_UserId",
                        column: x => x.UserId,
                        principalSchema: "Base",
                        principalTable: "User",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_PushEndpoint_UserId",
                schema: "Messaging",
                table: "PushEndpoint",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Round_UserId",
                schema: "Draw",
                table: "Round",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_RoundWinner_RoundId",
                schema: "Draw",
                table: "RoundWinner",
                column: "RoundId");

            migrationBuilder.CreateIndex(
                name: "IX_RoundWinner_UserId",
                schema: "Draw",
                table: "RoundWinner",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Round_User_UserId",
                schema: "Draw",
                table: "Round",
                column: "UserId",
                principalSchema: "Base",
                principalTable: "User",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_PushEndpoint_User_UserId",
                schema: "Messaging",
                table: "PushEndpoint",
                column: "UserId",
                principalSchema: "Base",
                principalTable: "User",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Round_User_UserId",
                schema: "Draw",
                table: "Round");

            migrationBuilder.DropForeignKey(
                name: "FK_PushEndpoint_User_UserId",
                schema: "Messaging",
                table: "PushEndpoint");

            migrationBuilder.DropTable(
                name: "RoundWinner",
                schema: "Draw");

            migrationBuilder.DropIndex(
                name: "IX_PushEndpoint_UserId",
                schema: "Messaging",
                table: "PushEndpoint");

            migrationBuilder.DropIndex(
                name: "IX_Round_UserId",
                schema: "Draw",
                table: "Round");

            migrationBuilder.DropColumn(
                name: "Priority",
                schema: "Messaging",
                table: "Notification");

            migrationBuilder.DropColumn(
                name: "UserId",
                schema: "Draw",
                table: "Round");

            migrationBuilder.AlterColumn<int>(
                name: "UserId",
                schema: "Messaging",
                table: "PushEndpoint",
                type: "int",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AddColumn<int>(
                name: "WinnerUserId",
                schema: "Draw",
                table: "Round",
                type: "int",
                nullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "UserId",
                schema: "Draw",
                table: "DrawChance",
                type: "int",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.CreateIndex(
                name: "IX_Round_WinnerUserId",
                schema: "Draw",
                table: "Round",
                column: "WinnerUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Round_User_WinnerUserId",
                schema: "Draw",
                table: "Round",
                column: "WinnerUserId",
                principalSchema: "Base",
                principalTable: "User",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
