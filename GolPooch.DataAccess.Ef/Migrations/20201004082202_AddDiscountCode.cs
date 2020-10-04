using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace GolPooch.DataAccess.Ef.Migrations
{
    public partial class AddDiscountCode : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "DiscountCode",
                schema: "Base",
                columns: table => new
                {
                    DiscountCodeId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<int>(nullable: true),
                    PurchaseId = table.Column<int>(nullable: true),
                    Type = table.Column<byte>(nullable: false),
                    IsUsed = table.Column<bool>(nullable: false),
                    UsedDateMi = table.Column<DateTime>(nullable: false),
                    UsedDateSh = table.Column<string>(type: "char(10)", maxLength: 10, nullable: true),
                    Code = table.Column<string>(type: "varchar(16)", maxLength: 16, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DiscountCode", x => x.DiscountCodeId);
                    table.ForeignKey(
                        name: "FK_DiscountCode_Purchase_PurchaseId",
                        column: x => x.PurchaseId,
                        principalSchema: "Product",
                        principalTable: "Purchase",
                        principalColumn: "PurchaseId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_DiscountCode_User_UserId",
                        column: x => x.UserId,
                        principalSchema: "Base",
                        principalTable: "User",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_DiscountCode_PurchaseId",
                schema: "Base",
                table: "DiscountCode",
                column: "PurchaseId");

            migrationBuilder.CreateIndex(
                name: "IX_DiscountCode_UserId",
                schema: "Base",
                table: "DiscountCode",
                column: "UserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DiscountCode",
                schema: "Base");
        }
    }
}
