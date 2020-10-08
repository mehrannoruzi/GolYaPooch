using Microsoft.EntityFrameworkCore.Migrations;

namespace GolPooch.DataAccess.Ef.Migrations
{
    public partial class AddRoundWinnerDrawChanceId : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "DrawChanceId",
                schema: "Draw",
                table: "RoundWinner",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_RoundWinner_DrawChanceId",
                schema: "Draw",
                table: "RoundWinner",
                column: "DrawChanceId");

            migrationBuilder.AddForeignKey(
                name: "FK_RoundWinner_DrawChance_DrawChanceId",
                schema: "Draw",
                table: "RoundWinner",
                column: "DrawChanceId",
                principalSchema: "Draw",
                principalTable: "DrawChance",
                principalColumn: "DrawChanceId",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_RoundWinner_DrawChance_DrawChanceId",
                schema: "Draw",
                table: "RoundWinner");

            migrationBuilder.DropIndex(
                name: "IX_RoundWinner_DrawChanceId",
                schema: "Draw",
                table: "RoundWinner");

            migrationBuilder.DropColumn(
                name: "DrawChanceId",
                schema: "Draw",
                table: "RoundWinner");
        }
    }
}
