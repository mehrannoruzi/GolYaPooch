using Microsoft.EntityFrameworkCore.Migrations;

namespace GolPooch.DataAccess.Ef.Migrations
{
    public partial class RemoveRoundUserId : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Round_User_UserId",
                schema: "Draw",
                table: "Round");

            migrationBuilder.DropIndex(
                name: "IX_Round_UserId",
                schema: "Draw",
                table: "Round");

            migrationBuilder.DropColumn(
                name: "UserId",
                schema: "Draw",
                table: "Round");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "UserId",
                schema: "Draw",
                table: "Round",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Round_UserId",
                schema: "Draw",
                table: "Round",
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
        }
    }
}
