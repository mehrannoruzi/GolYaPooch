using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace GolPooch.DataAccess.Ef.Migrations
{
    public partial class changeUserDevice2ActivityLog : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "UserDeviceLog",
                schema: "Base");

            //migrationBuilder.CreateTable(
            //    name: "WinnerDto",
            //    columns: table => new
            //    {
            //        Number = table.Column<long>(nullable: false),
            //        FullName = table.Column<string>(nullable: true),
            //        MobileNumber = table.Column<string>(nullable: true),
            //        ChanceCount = table.Column<int>(nullable: false),
            //        WinCount = table.Column<int>(nullable: false)
            //    },
            //    constraints: table =>
            //    {
            //    });

            migrationBuilder.CreateTable(
                name: "ActivityLog",
                schema: "Base",
                columns: table => new
                {
                    ActivityLogId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MobileNumber = table.Column<long>(nullable: false),
                    Type = table.Column<byte>(nullable: false),
                    IsMobile = table.Column<bool>(nullable: false),
                    InsertDateMi = table.Column<DateTime>(nullable: false),
                    InsertDateSh = table.Column<string>(type: "char(10)", maxLength: 10, nullable: true),
                    IP = table.Column<string>(type: "varchar(20)", maxLength: 20, nullable: true),
                    Os = table.Column<string>(type: "varchar(20)", maxLength: 20, nullable: true),
                    Device = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: true),
                    Application = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ActivityLog", x => x.ActivityLogId);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            //migrationBuilder.DropTable(
            //    name: "WinnerDto");

            migrationBuilder.DropTable(
                name: "ActivityLog",
                schema: "Base");

            migrationBuilder.CreateTable(
                name: "UserDeviceLog",
                schema: "Base",
                columns: table => new
                {
                    UserDeviceLogId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Application = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: true),
                    Device = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: true),
                    IP = table.Column<string>(type: "varchar(20)", maxLength: 20, nullable: true),
                    InsertDateMi = table.Column<DateTime>(type: "datetime2", nullable: false),
                    InsertDateSh = table.Column<string>(type: "char(10)", maxLength: 10, nullable: true),
                    IsMobile = table.Column<bool>(type: "bit", nullable: false),
                    MobileNumber = table.Column<long>(type: "bigint", nullable: false),
                    Os = table.Column<string>(type: "varchar(20)", maxLength: 20, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserDeviceLog", x => x.UserDeviceLogId);
                });
        }
    }
}
