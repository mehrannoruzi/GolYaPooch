using Microsoft.EntityFrameworkCore.Migrations;

namespace GolPooch.DataAccess.Ef.Migrations
{
    public partial class EditRoundWinnerDrawChanceId : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "DrawChanceId",
                schema: "Draw",
                table: "RoundWinner",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "DrawChanceId",
                schema: "Draw",
                table: "RoundWinner",
                type: "int",
                nullable: true,
                oldClrType: typeof(int));
        }
    }
}
