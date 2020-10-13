using Microsoft.EntityFrameworkCore.Migrations;

namespace GolPooch.DataAccess.Ef.Migrations
{
    public partial class AddUser_Balance : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Balance",
                schema: "Base",
                table: "User",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Balance",
                schema: "Base",
                table: "User");
        }
    }
}
