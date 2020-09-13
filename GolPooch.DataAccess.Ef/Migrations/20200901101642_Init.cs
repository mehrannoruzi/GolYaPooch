using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace GolPooch.DataAccess.Ef.Migrations
{
    public partial class Init : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "Messaging");

            migrationBuilder.EnsureSchema(
                name: "Base");

            migrationBuilder.EnsureSchema(
                name: "Draw");

            migrationBuilder.EnsureSchema(
                name: "Payment");

            migrationBuilder.EnsureSchema(
                name: "Product");

            migrationBuilder.CreateTable(
                name: "ChangeLog",
                schema: "Base",
                columns: table => new
                {
                    ChangeLogId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    InsertDateMi = table.Column<DateTime>(nullable: false),
                    InsertDateSh = table.Column<string>(type: "char(10)", maxLength: 10, nullable: true),
                    Version = table.Column<string>(type: "varchar(8)", maxLength: 8, nullable: false),
                    Changes = table.Column<string>(maxLength: 2000, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ChangeLog", x => x.ChangeLogId);
                });

            migrationBuilder.CreateTable(
                name: "Page",
                schema: "Base",
                columns: table => new
                {
                    PageId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IsActive = table.Column<bool>(nullable: false),
                    Address = table.Column<string>(type: "varchar(30)", maxLength: 30, nullable: false),
                    Name = table.Column<string>(maxLength: 30, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Page", x => x.PageId);
                });

            migrationBuilder.CreateTable(
                name: "User",
                schema: "Base",
                columns: table => new
                {
                    UserId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MobileNumber = table.Column<long>(nullable: false),
                    OsType = table.Column<int>(nullable: false),
                    Region = table.Column<int>(nullable: false),
                    Gender = table.Column<byte>(nullable: false),
                    InsertDateMi = table.Column<DateTime>(nullable: false),
                    InsertDateSh = table.Column<string>(type: "char(10)", maxLength: 10, nullable: true),
                    BirthdateMi = table.Column<DateTime>(nullable: false),
                    BirthdateSh = table.Column<string>(type: "char(10)", maxLength: 10, nullable: true),
                    FirstName = table.Column<string>(maxLength: 25, nullable: false),
                    LastName = table.Column<string>(maxLength: 30, nullable: false),
                    Email = table.Column<string>(type: "varchar(150)", maxLength: 150, nullable: false),
                    PushId = table.Column<string>(type: "varchar(150)", maxLength: 150, nullable: false),
                    ProfileImgUrl = table.Column<string>(type: "varchar(250)", maxLength: 250, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_User", x => x.UserId);
                });

            migrationBuilder.CreateTable(
                name: "Chest",
                schema: "Draw",
                columns: table => new
                {
                    ChestId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoundCount = table.Column<int>(nullable: false),
                    ParticipantCount = table.Column<int>(nullable: false),
                    WinnerCount = table.Column<int>(nullable: false),
                    IsActive = table.Column<bool>(nullable: false),
                    InsertDateMi = table.Column<DateTime>(nullable: false),
                    InsertDateSh = table.Column<string>(type: "char(10)", maxLength: 10, nullable: true),
                    Title = table.Column<string>(maxLength: 35, nullable: false),
                    ImageUrl = table.Column<string>(type: "varchar(250)", maxLength: 30, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Chest", x => x.ChestId);
                });

            migrationBuilder.CreateTable(
                name: "Gateway",
                schema: "Payment",
                columns: table => new
                {
                    PaymentGatewayId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BankName = table.Column<byte>(nullable: false),
                    IsActive = table.Column<bool>(nullable: false),
                    InsertDateMi = table.Column<DateTime>(nullable: false),
                    InsertDateSh = table.Column<string>(type: "char(10)", maxLength: 10, nullable: true),
                    Name = table.Column<string>(maxLength: 25, nullable: false),
                    Username = table.Column<string>(type: "varchar(25)", maxLength: 25, nullable: false),
                    Password = table.Column<string>(type: "varchar(25)", maxLength: 25, nullable: false),
                    MerchantId = table.Column<string>(type: "varchar(36)", maxLength: 36, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Gateway", x => x.PaymentGatewayId);
                });

            migrationBuilder.CreateTable(
                name: "Product",
                schema: "Product",
                columns: table => new
                {
                    ProductId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IsShow = table.Column<bool>(nullable: false),
                    InsertDateMi = table.Column<DateTime>(nullable: false),
                    InsertDateSh = table.Column<string>(type: "char(10)", maxLength: 10, nullable: true),
                    ExpirationDate = table.Column<DateTime>(nullable: false),
                    Subject = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false),
                    Text = table.Column<string>(type: "varchar(150)", maxLength: 150, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Product", x => x.ProductId);
                });

            migrationBuilder.CreateTable(
                name: "Banner",
                schema: "Messaging",
                columns: table => new
                {
                    BannerId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PageId = table.Column<int>(nullable: false),
                    IsActive = table.Column<bool>(nullable: false),
                    Type = table.Column<byte>(nullable: false),
                    ActionType = table.Column<byte>(nullable: false),
                    DisplayType = table.Column<byte>(nullable: false),
                    LocationType = table.Column<byte>(nullable: false),
                    ExpirationDate = table.Column<DateTime>(nullable: false),
                    InsertDateMi = table.Column<DateTime>(nullable: false),
                    InsertDateSh = table.Column<string>(type: "char(10)", maxLength: 10, nullable: true),
                    ActionText = table.Column<string>(type: "varchar(30)", maxLength: 30, nullable: false),
                    Subject = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false),
                    ImageUrl = table.Column<string>(type: "varchar(250)", maxLength: 250, nullable: false),
                    Href = table.Column<string>(type: "varchar(250)", maxLength: 250, nullable: false),
                    Text = table.Column<string>(type: "varchar(500)", maxLength: 500, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Banner", x => x.BannerId);
                    table.ForeignKey(
                        name: "FK_Banner_Page_PageId",
                        column: x => x.PageId,
                        principalSchema: "Base",
                        principalTable: "Page",
                        principalColumn: "PageId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Ticket",
                schema: "Base",
                columns: table => new
                {
                    TicketId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<int>(nullable: false),
                    AnswerAdminId = table.Column<Guid>(nullable: true),
                    Type = table.Column<int>(nullable: false),
                    InsertDateMi = table.Column<DateTime>(nullable: false),
                    InsertDateSh = table.Column<string>(type: "char(10)", maxLength: 10, nullable: true),
                    AnswerDateMi = table.Column<DateTime>(nullable: false),
                    AnswerDateSh = table.Column<string>(type: "char(10)", maxLength: 10, nullable: true),
                    Text = table.Column<string>(maxLength: 1000, nullable: false),
                    Answer = table.Column<string>(maxLength: 1000, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Ticket", x => x.TicketId);
                    table.ForeignKey(
                        name: "FK_Ticket_User_UserId",
                        column: x => x.UserId,
                        principalSchema: "Base",
                        principalTable: "User",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "VerificationCode",
                schema: "Base",
                columns: table => new
                {
                    VerificationCodeId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<int>(nullable: false),
                    PinCode = table.Column<int>(nullable: false),
                    IsUsed = table.Column<bool>(nullable: false),
                    InsertDateMi = table.Column<DateTime>(nullable: false),
                    InsertDateSh = table.Column<string>(type: "char(10)", maxLength: 10, nullable: true),
                    ExpirationTime = table.Column<DateTime>(nullable: false)
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

            migrationBuilder.CreateTable(
                name: "Notification",
                schema: "Messaging",
                columns: table => new
                {
                    NotificationId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<int>(nullable: true),
                    Type = table.Column<byte>(nullable: false),
                    Action = table.Column<byte>(nullable: false),
                    IsActive = table.Column<bool>(nullable: false),
                    IsSent = table.Column<bool>(nullable: false),
                    IsSuccess = table.Column<bool>(nullable: true),
                    IsRead = table.Column<bool>(nullable: false),
                    InsertDateMi = table.Column<DateTime>(nullable: false),
                    InsertDateSh = table.Column<string>(type: "char(10)", maxLength: 10, nullable: true),
                    SentDateMi = table.Column<DateTime>(nullable: false),
                    ActionText = table.Column<string>(type: "varchar(30)", maxLength: 30, nullable: false),
                    Subject = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false),
                    ImageUrl = table.Column<string>(type: "varchar(250)", maxLength: 250, nullable: false),
                    ActionUrl = table.Column<string>(type: "varchar(250)", maxLength: 250, nullable: false),
                    Text = table.Column<string>(type: "varchar(500)", maxLength: 500, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Notification", x => x.NotificationId);
                    table.ForeignKey(
                        name: "FK_Notification_User_UserId",
                        column: x => x.UserId,
                        principalSchema: "Base",
                        principalTable: "User",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Round",
                schema: "Draw",
                columns: table => new
                {
                    RoundId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ChestId = table.Column<int>(nullable: false),
                    WinnerUserId = table.Column<int>(nullable: false),
                    ParticipantCount = table.Column<int>(nullable: false),
                    State = table.Column<byte>(nullable: false),
                    StartDateMi = table.Column<DateTime>(nullable: false),
                    StartDateSh = table.Column<string>(type: "char(10)", maxLength: 10, nullable: true),
                    EndDateMi = table.Column<DateTime>(nullable: false),
                    EndDateSh = table.Column<string>(type: "char(10)", maxLength: 10, nullable: true),
                    InsertDateMi = table.Column<DateTime>(nullable: false),
                    InsertDateSh = table.Column<string>(type: "char(10)", maxLength: 10, nullable: true),
                    ModifyDateMi = table.Column<DateTime>(nullable: false),
                    ModifyDateSh = table.Column<string>(type: "char(10)", maxLength: 10, nullable: true),
                    Description = table.Column<string>(maxLength: 250, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Round", x => x.RoundId);
                    table.ForeignKey(
                        name: "FK_Round_Chest_ChestId",
                        column: x => x.ChestId,
                        principalSchema: "Draw",
                        principalTable: "Chest",
                        principalColumn: "ChestId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Round_User_WinnerUserId",
                        column: x => x.WinnerUserId,
                        principalSchema: "Base",
                        principalTable: "User",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ProductOffer",
                schema: "Product",
                columns: table => new
                {
                    ProductOfferId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProductId = table.Column<int>(nullable: false),
                    Chance = table.Column<byte>(nullable: false),
                    Price = table.Column<int>(nullable: false),
                    Discount = table.Column<int>(nullable: false),
                    TotalPrice = table.Column<int>(nullable: false),
                    Profit = table.Column<int>(nullable: false),
                    UnUseDay = table.Column<int>(nullable: false),
                    IsActive = table.Column<bool>(nullable: false),
                    InsertDateMi = table.Column<DateTime>(nullable: false),
                    InsertDateSh = table.Column<string>(type: "char(10)", maxLength: 10, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductOffer", x => x.ProductOfferId);
                    table.ForeignKey(
                        name: "FK_ProductOffer_Product_ProductId",
                        column: x => x.ProductId,
                        principalSchema: "Product",
                        principalTable: "Product",
                        principalColumn: "ProductId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "NotificationDelivery",
                schema: "Messaging",
                columns: table => new
                {
                    NotificationDeliveryId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NotificationId = table.Column<int>(nullable: false),
                    UserId = table.Column<int>(nullable: false),
                    Type = table.Column<int>(nullable: false),
                    InsertDateMi = table.Column<DateTime>(nullable: false),
                    InsertDateSh = table.Column<string>(type: "char(10)", maxLength: 10, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NotificationDelivery", x => x.NotificationDeliveryId);
                    table.ForeignKey(
                        name: "FK_NotificationDelivery_Notification_NotificationId",
                        column: x => x.NotificationId,
                        principalSchema: "Messaging",
                        principalTable: "Notification",
                        principalColumn: "NotificationId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_NotificationDelivery_User_UserId",
                        column: x => x.UserId,
                        principalSchema: "Base",
                        principalTable: "User",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Transaction",
                schema: "Payment",
                columns: table => new
                {
                    PaymentTransactionId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<int>(nullable: false),
                    PaymentGatewayId = table.Column<int>(nullable: false),
                    ProductOfferId = table.Column<int>(nullable: false),
                    Type = table.Column<byte>(nullable: false),
                    Price = table.Column<int>(nullable: false),
                    IsSuccess = table.Column<bool>(nullable: false),
                    InsertDateMi = table.Column<DateTime>(nullable: false),
                    InsertDateSh = table.Column<string>(type: "char(10)", maxLength: 10, nullable: true),
                    ModifyDateMi = table.Column<DateTime>(nullable: false),
                    ModifyDateSh = table.Column<string>(type: "char(10)", maxLength: 10, nullable: true),
                    UserSheba = table.Column<string>(type: "varchar(21)", maxLength: 21, nullable: false),
                    Status = table.Column<string>(type: "varchar(30)", maxLength: 30, nullable: false),
                    TrackingId = table.Column<string>(type: "varchar(36)", maxLength: 36, nullable: false),
                    Description = table.Column<string>(type: "varchar(150)", maxLength: 150, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Transaction", x => x.PaymentTransactionId);
                    table.ForeignKey(
                        name: "FK_Transaction_Gateway_PaymentGatewayId",
                        column: x => x.PaymentGatewayId,
                        principalSchema: "Payment",
                        principalTable: "Gateway",
                        principalColumn: "PaymentGatewayId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Transaction_ProductOffer_ProductOfferId",
                        column: x => x.ProductOfferId,
                        principalSchema: "Product",
                        principalTable: "ProductOffer",
                        principalColumn: "ProductOfferId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Transaction_User_UserId",
                        column: x => x.UserId,
                        principalSchema: "Base",
                        principalTable: "User",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Purchase",
                schema: "Product",
                columns: table => new
                {
                    PurchaseId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<int>(nullable: false),
                    ProductOfferId = table.Column<int>(nullable: false),
                    PaymentTransactionId = table.Column<int>(nullable: false),
                    Chance = table.Column<byte>(nullable: false),
                    UsedChance = table.Column<byte>(nullable: false),
                    IsReFoundable = table.Column<bool>(nullable: false),
                    IsFinished = table.Column<bool>(nullable: false),
                    InsertDateMi = table.Column<DateTime>(nullable: false),
                    InsertDateSh = table.Column<string>(type: "char(10)", maxLength: 10, nullable: true),
                    ModifyDateMi = table.Column<DateTime>(nullable: false),
                    ModifyDateSh = table.Column<string>(type: "char(10)", maxLength: 10, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Purchase", x => x.PurchaseId);
                    table.ForeignKey(
                        name: "FK_Purchase_Transaction_PaymentTransactionId",
                        column: x => x.PaymentTransactionId,
                        principalSchema: "Payment",
                        principalTable: "Transaction",
                        principalColumn: "PaymentTransactionId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Purchase_ProductOffer_ProductOfferId",
                        column: x => x.ProductOfferId,
                        principalSchema: "Product",
                        principalTable: "ProductOffer",
                        principalColumn: "ProductOfferId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Purchase_User_UserId",
                        column: x => x.UserId,
                        principalSchema: "Base",
                        principalTable: "User",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "DrawChance",
                schema: "Draw",
                columns: table => new
                {
                    DrawChanceId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<int>(nullable: true),
                    PurchaseId = table.Column<int>(nullable: false),
                    RoundId = table.Column<int>(nullable: false),
                    Counter = table.Column<int>(nullable: false),
                    InsertDateMi = table.Column<DateTime>(nullable: false),
                    InsertDateSh = table.Column<string>(type: "char(10)", maxLength: 10, nullable: true),
                    Code = table.Column<string>(type: "varchar(16)", maxLength: 16, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DrawChance", x => x.DrawChanceId);
                    table.ForeignKey(
                        name: "FK_DrawChance_Purchase_PurchaseId",
                        column: x => x.PurchaseId,
                        principalSchema: "Product",
                        principalTable: "Purchase",
                        principalColumn: "PurchaseId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_DrawChance_Round_RoundId",
                        column: x => x.RoundId,
                        principalSchema: "Draw",
                        principalTable: "Round",
                        principalColumn: "RoundId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_DrawChance_User_UserId",
                        column: x => x.UserId,
                        principalSchema: "Base",
                        principalTable: "User",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Page_Address",
                schema: "Base",
                table: "Page",
                column: "Address",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Ticket_UserId",
                schema: "Base",
                table: "Ticket",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_User_MobileNumber",
                schema: "Base",
                table: "User",
                column: "MobileNumber",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_VerificationCode_UserId",
                schema: "Base",
                table: "VerificationCode",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_DrawChance_PurchaseId",
                schema: "Draw",
                table: "DrawChance",
                column: "PurchaseId");

            migrationBuilder.CreateIndex(
                name: "IX_DrawChance_RoundId",
                schema: "Draw",
                table: "DrawChance",
                column: "RoundId");

            migrationBuilder.CreateIndex(
                name: "IX_DrawChance_UserId",
                schema: "Draw",
                table: "DrawChance",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Round_ChestId",
                schema: "Draw",
                table: "Round",
                column: "ChestId");

            migrationBuilder.CreateIndex(
                name: "IX_Round_WinnerUserId",
                schema: "Draw",
                table: "Round",
                column: "WinnerUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Banner_PageId",
                schema: "Messaging",
                table: "Banner",
                column: "PageId");

            migrationBuilder.CreateIndex(
                name: "IX_Notification_UserId",
                schema: "Messaging",
                table: "Notification",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_NotificationDelivery_NotificationId",
                schema: "Messaging",
                table: "NotificationDelivery",
                column: "NotificationId");

            migrationBuilder.CreateIndex(
                name: "IX_NotificationDelivery_UserId",
                schema: "Messaging",
                table: "NotificationDelivery",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Transaction_PaymentGatewayId",
                schema: "Payment",
                table: "Transaction",
                column: "PaymentGatewayId");

            migrationBuilder.CreateIndex(
                name: "IX_Transaction_ProductOfferId",
                schema: "Payment",
                table: "Transaction",
                column: "ProductOfferId");

            migrationBuilder.CreateIndex(
                name: "IX_Transaction_UserId",
                schema: "Payment",
                table: "Transaction",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductOffer_ProductId",
                schema: "Product",
                table: "ProductOffer",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_Purchase_PaymentTransactionId",
                schema: "Product",
                table: "Purchase",
                column: "PaymentTransactionId");

            migrationBuilder.CreateIndex(
                name: "IX_Purchase_ProductOfferId",
                schema: "Product",
                table: "Purchase",
                column: "ProductOfferId");

            migrationBuilder.CreateIndex(
                name: "IX_Purchase_UserId",
                schema: "Product",
                table: "Purchase",
                column: "UserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ChangeLog",
                schema: "Base");

            migrationBuilder.DropTable(
                name: "Ticket",
                schema: "Base");

            migrationBuilder.DropTable(
                name: "VerificationCode",
                schema: "Base");

            migrationBuilder.DropTable(
                name: "DrawChance",
                schema: "Draw");

            migrationBuilder.DropTable(
                name: "Banner",
                schema: "Messaging");

            migrationBuilder.DropTable(
                name: "NotificationDelivery",
                schema: "Messaging");

            migrationBuilder.DropTable(
                name: "Purchase",
                schema: "Product");

            migrationBuilder.DropTable(
                name: "Round",
                schema: "Draw");

            migrationBuilder.DropTable(
                name: "Page",
                schema: "Base");

            migrationBuilder.DropTable(
                name: "Notification",
                schema: "Messaging");

            migrationBuilder.DropTable(
                name: "Transaction",
                schema: "Payment");

            migrationBuilder.DropTable(
                name: "Chest",
                schema: "Draw");

            migrationBuilder.DropTable(
                name: "Gateway",
                schema: "Payment");

            migrationBuilder.DropTable(
                name: "ProductOffer",
                schema: "Product");

            migrationBuilder.DropTable(
                name: "User",
                schema: "Base");

            migrationBuilder.DropTable(
                name: "Product",
                schema: "Product");
        }
    }
}
