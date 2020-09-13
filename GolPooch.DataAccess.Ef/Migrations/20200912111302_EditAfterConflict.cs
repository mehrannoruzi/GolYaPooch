using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace GolPooch.DataAccess.Ef.Migrations
{
    public partial class EditAfterConflict : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Banner_Page_PageId",
                schema: "Messaging",
                table: "Banner");

            migrationBuilder.DropTable(
                name: "VerificationCode",
                schema: "Base");

            migrationBuilder.CreateTable(
                name: "Authenticate",
                schema: "Base",
                columns: table => new
                {
                    AuthenticateId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MobileNumber = table.Column<long>(nullable: false),
                    PinCode = table.Column<int>(nullable: false),
                    IsUsed = table.Column<bool>(nullable: false),
                    InsertDateMi = table.Column<DateTime>(nullable: false),
                    InsertDateSh = table.Column<string>(type: "char(10)", maxLength: 10, nullable: true),
                    ExpirationTime = table.Column<DateTime>(nullable: false),
                    UsedTime = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Authenticate", x => x.AuthenticateId);
                });

            migrationBuilder.AddForeignKey(
                name: "FK_Banner_Page_PageId",
                schema: "Messaging",
                table: "Banner",
                column: "PageId",
                principalSchema: "Base",
                principalTable: "Page",
                principalColumn: "PageId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Banner_Page_PageId",
                schema: "Messaging",
                table: "Banner");

            migrationBuilder.DropTable(
                name: "Authenticate",
                schema: "Base");

            migrationBuilder.CreateTable(
                name: "VerificationCode",
                schema: "Base",
                columns: table => new
                {
                    VerificationCodeId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ExpirationTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    InsertDateMi = table.Column<DateTime>(type: "datetime2", nullable: false),
                    InsertDateSh = table.Column<string>(type: "char(10)", maxLength: 10, nullable: true),
                    IsUsed = table.Column<bool>(type: "bit", nullable: false),
                    MobileNumber = table.Column<long>(type: "bigint", nullable: false),
                    PinCode = table.Column<int>(type: "int", nullable: false),
                    UserId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VerificationCode", x => x.VerificationCodeId);
                    table.ForeignKey(
                        name: "FK_VerificationCode_User_UserId",
                        column: x => x.UserId,
                        principalSchema: "Base",
                        principalTable: "User",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_VerificationCode_UserId",
                schema: "Base",
                table: "VerificationCode",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Banner_Page_PageId",
                schema: "Messaging",
                table: "Banner",
                column: "PageId",
                principalSchema: "Base",
                principalTable: "Page",
                principalColumn: "PageId",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
