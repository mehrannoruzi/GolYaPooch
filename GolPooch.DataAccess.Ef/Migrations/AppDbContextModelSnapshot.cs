﻿// <auto-generated />
using System;
using GolPooch.DataAccess.Ef;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace GolPooch.DataAccess.Ef.Migrations
{
    [DbContext(typeof(AppDbContext))]
    partial class AppDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.7")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("GolPooch.Domain.Entity.Authenticate", b =>
                {
                    b.Property<int>("AuthenticateId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("ExpirationTime")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("InsertDateMi")
                        .HasColumnType("datetime2");

                    b.Property<string>("InsertDateSh")
                        .HasColumnType("char(10)")
                        .HasMaxLength(10);

                    b.Property<bool>("IsUsed")
                        .HasColumnType("bit");

                    b.Property<long>("MobileNumber")
                        .HasColumnType("bigint");

                    b.Property<int>("PinCode")
                        .HasColumnType("int");

                    b.Property<DateTime>("UsedTime")
                        .HasColumnType("datetime2");

                    b.HasKey("AuthenticateId");

                    b.ToTable("Authenticate","Base");
                });

            modelBuilder.Entity("GolPooch.Domain.Entity.Banner", b =>
                {
                    b.Property<int>("BannerId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("ActionText")
                        .IsRequired()
                        .HasColumnType("varchar(30)")
                        .HasMaxLength(30);

                    b.Property<byte>("ActionType")
                        .HasColumnType("tinyint");

                    b.Property<byte>("DisplayType")
                        .HasColumnType("tinyint");

                    b.Property<DateTime>("ExpirationDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Href")
                        .IsRequired()
                        .HasColumnType("varchar(250)")
                        .HasMaxLength(250);

                    b.Property<string>("ImageUrl")
                        .IsRequired()
                        .HasColumnType("varchar(250)")
                        .HasMaxLength(250);

                    b.Property<DateTime>("InsertDateMi")
                        .HasColumnType("datetime2");

                    b.Property<string>("InsertDateSh")
                        .HasColumnType("char(10)")
                        .HasMaxLength(10);

                    b.Property<bool>("IsActive")
                        .HasColumnType("bit");

                    b.Property<byte>("LocationType")
                        .HasColumnType("tinyint");

                    b.Property<int>("PageId")
                        .HasColumnType("int");

                    b.Property<string>("Subject")
                        .IsRequired()
                        .HasColumnType("varchar(50)")
                        .HasMaxLength(50);

                    b.Property<string>("Text")
                        .IsRequired()
                        .HasColumnType("varchar(500)")
                        .HasMaxLength(500);

                    b.Property<byte>("Type")
                        .HasColumnType("tinyint");

                    b.HasKey("BannerId");

                    b.HasIndex("PageId");

                    b.ToTable("Banner","Messaging");
                });

            modelBuilder.Entity("GolPooch.Domain.Entity.ChangeLog", b =>
                {
                    b.Property<int>("ChangeLogId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Changes")
                        .IsRequired()
                        .HasColumnType("nvarchar(2000)")
                        .HasMaxLength(2000);

                    b.Property<DateTime>("InsertDateMi")
                        .HasColumnType("datetime2");

                    b.Property<string>("InsertDateSh")
                        .HasColumnType("char(10)")
                        .HasMaxLength(10);

                    b.Property<string>("Version")
                        .IsRequired()
                        .HasColumnType("varchar(8)")
                        .HasMaxLength(8);

                    b.HasKey("ChangeLogId");

                    b.ToTable("ChangeLog","Base");
                });

            modelBuilder.Entity("GolPooch.Domain.Entity.Chest", b =>
                {
                    b.Property<int>("ChestId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("ImageUrl")
                        .IsRequired()
                        .HasColumnType("varchar(250)")
                        .HasMaxLength(30);

                    b.Property<DateTime>("InsertDateMi")
                        .HasColumnType("datetime2");

                    b.Property<string>("InsertDateSh")
                        .HasColumnType("char(10)")
                        .HasMaxLength(10);

                    b.Property<bool>("IsActive")
                        .HasColumnType("bit");

                    b.Property<int>("ParticipantCount")
                        .HasColumnType("int");

                    b.Property<int>("RoundCount")
                        .HasColumnType("int");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("nvarchar(35)")
                        .HasMaxLength(35);

                    b.Property<int>("WinnerCount")
                        .HasColumnType("int");

                    b.HasKey("ChestId");

                    b.ToTable("Chest","Draw");
                });

            modelBuilder.Entity("GolPooch.Domain.Entity.DrawChance", b =>
                {
                    b.Property<int>("DrawChanceId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Code")
                        .IsRequired()
                        .HasColumnType("varchar(16)")
                        .HasMaxLength(16);

                    b.Property<int>("Counter")
                        .HasColumnType("int");

                    b.Property<DateTime>("InsertDateMi")
                        .HasColumnType("datetime2");

                    b.Property<string>("InsertDateSh")
                        .HasColumnType("char(10)")
                        .HasMaxLength(10);

                    b.Property<int>("PurchaseId")
                        .HasColumnType("int");

                    b.Property<int>("RoundId")
                        .HasColumnType("int");

                    b.Property<int?>("UserId")
                        .HasColumnType("int");

                    b.HasKey("DrawChanceId");

                    b.HasIndex("PurchaseId");

                    b.HasIndex("RoundId");

                    b.HasIndex("UserId");

                    b.ToTable("DrawChance","Draw");
                });

            modelBuilder.Entity("GolPooch.Domain.Entity.Notification", b =>
                {
                    b.Property<int>("NotificationId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<byte>("Action")
                        .HasColumnType("tinyint");

                    b.Property<string>("ActionText")
                        .IsRequired()
                        .HasColumnType("varchar(30)")
                        .HasMaxLength(30);

                    b.Property<string>("ActionUrl")
                        .IsRequired()
                        .HasColumnType("varchar(250)")
                        .HasMaxLength(250);

                    b.Property<string>("ImageUrl")
                        .IsRequired()
                        .HasColumnType("varchar(250)")
                        .HasMaxLength(250);

                    b.Property<DateTime>("InsertDateMi")
                        .HasColumnType("datetime2");

                    b.Property<string>("InsertDateSh")
                        .HasColumnType("char(10)")
                        .HasMaxLength(10);

                    b.Property<bool>("IsActive")
                        .HasColumnType("bit");

                    b.Property<bool>("IsRead")
                        .HasColumnType("bit");

                    b.Property<bool>("IsSent")
                        .HasColumnType("bit");

                    b.Property<bool?>("IsSuccess")
                        .HasColumnType("bit");

                    b.Property<DateTime>("SentDateMi")
                        .HasColumnType("datetime2");

                    b.Property<string>("Subject")
                        .IsRequired()
                        .HasColumnType("varchar(50)")
                        .HasMaxLength(50);

                    b.Property<string>("Text")
                        .IsRequired()
                        .HasColumnType("varchar(500)")
                        .HasMaxLength(500);

                    b.Property<byte>("Type")
                        .HasColumnType("tinyint");

                    b.Property<int?>("UserId")
                        .HasColumnType("int");

                    b.HasKey("NotificationId");

                    b.HasIndex("UserId");

                    b.ToTable("Notification","Messaging");
                });

            modelBuilder.Entity("GolPooch.Domain.Entity.NotificationDelivery", b =>
                {
                    b.Property<int>("NotificationDeliveryId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("InsertDateMi")
                        .HasColumnType("datetime2");

                    b.Property<string>("InsertDateSh")
                        .HasColumnType("char(10)")
                        .HasMaxLength(10);

                    b.Property<int>("NotificationId")
                        .HasColumnType("int");

                    b.Property<int>("Type")
                        .HasColumnType("int");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.HasKey("NotificationDeliveryId");

                    b.HasIndex("NotificationId");

                    b.HasIndex("UserId");

                    b.ToTable("NotificationDelivery","Messaging");
                });

            modelBuilder.Entity("GolPooch.Domain.Entity.Page", b =>
                {
                    b.Property<int>("PageId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Address")
                        .IsRequired()
                        .HasColumnType("varchar(30)")
                        .HasMaxLength(30);

                    b.Property<bool>("IsActive")
                        .HasColumnType("bit");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(30)")
                        .HasMaxLength(30);

                    b.HasKey("PageId");

                    b.HasIndex("Address")
                        .IsUnique();

                    b.ToTable("Page","Base");
                });

            modelBuilder.Entity("GolPooch.Domain.Entity.PaymentGateway", b =>
                {
                    b.Property<int>("PaymentGatewayId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<byte>("BankName")
                        .HasColumnType("tinyint");

                    b.Property<DateTime>("InsertDateMi")
                        .HasColumnType("datetime2");

                    b.Property<string>("InsertDateSh")
                        .HasColumnType("char(10)")
                        .HasMaxLength(10);

                    b.Property<bool>("IsActive")
                        .HasColumnType("bit");

                    b.Property<string>("MerchantId")
                        .IsRequired()
                        .HasColumnType("varchar(36)")
                        .HasMaxLength(36);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(25)")
                        .HasMaxLength(25);

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("varchar(25)")
                        .HasMaxLength(25);

                    b.Property<string>("Username")
                        .IsRequired()
                        .HasColumnType("varchar(25)")
                        .HasMaxLength(25);

                    b.HasKey("PaymentGatewayId");

                    b.ToTable("Gateway","Payment");
                });

            modelBuilder.Entity("GolPooch.Domain.Entity.PaymentTransaction", b =>
                {
                    b.Property<int>("PaymentTransactionId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("varchar(150)")
                        .HasMaxLength(150);

                    b.Property<DateTime>("InsertDateMi")
                        .HasColumnType("datetime2");

                    b.Property<string>("InsertDateSh")
                        .HasColumnType("char(10)")
                        .HasMaxLength(10);

                    b.Property<bool>("IsSuccess")
                        .HasColumnType("bit");

                    b.Property<DateTime>("ModifyDateMi")
                        .HasColumnType("datetime2");

                    b.Property<string>("ModifyDateSh")
                        .HasColumnType("char(10)")
                        .HasMaxLength(10);

                    b.Property<int>("PaymentGatewayId")
                        .HasColumnType("int");

                    b.Property<int>("Price")
                        .HasColumnType("int");

                    b.Property<int>("ProductOfferId")
                        .HasColumnType("int");

                    b.Property<string>("Status")
                        .IsRequired()
                        .HasColumnType("varchar(30)")
                        .HasMaxLength(30);

                    b.Property<string>("TrackingId")
                        .IsRequired()
                        .HasColumnType("varchar(36)")
                        .HasMaxLength(36);

                    b.Property<byte>("Type")
                        .HasColumnType("tinyint");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.Property<string>("UserSheba")
                        .IsRequired()
                        .HasColumnType("varchar(21)")
                        .HasMaxLength(21);

                    b.HasKey("PaymentTransactionId");

                    b.HasIndex("PaymentGatewayId");

                    b.HasIndex("ProductOfferId");

                    b.HasIndex("UserId");

                    b.ToTable("Transaction","Payment");
                });

            modelBuilder.Entity("GolPooch.Domain.Entity.Product", b =>
                {
                    b.Property<int>("ProductId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("ExpirationDate")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("InsertDateMi")
                        .HasColumnType("datetime2");

                    b.Property<string>("InsertDateSh")
                        .HasColumnType("char(10)")
                        .HasMaxLength(10);

                    b.Property<bool>("IsShow")
                        .HasColumnType("bit");

                    b.Property<string>("Subject")
                        .IsRequired()
                        .HasColumnType("varchar(50)")
                        .HasMaxLength(50);

                    b.Property<string>("Text")
                        .IsRequired()
                        .HasColumnType("varchar(150)")
                        .HasMaxLength(150);

                    b.Property<int>("Type")
                        .HasColumnType("int");

                    b.HasKey("ProductId");

                    b.ToTable("Product","Product");
                });

            modelBuilder.Entity("GolPooch.Domain.Entity.ProductOffer", b =>
                {
                    b.Property<int>("ProductOfferId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<byte>("Chance")
                        .HasColumnType("tinyint");

                    b.Property<int>("Discount")
                        .HasColumnType("int");

                    b.Property<DateTime>("InsertDateMi")
                        .HasColumnType("datetime2");

                    b.Property<string>("InsertDateSh")
                        .HasColumnType("char(10)")
                        .HasMaxLength(10);

                    b.Property<bool>("IsActive")
                        .HasColumnType("bit");

                    b.Property<int>("Price")
                        .HasColumnType("int");

                    b.Property<int>("ProductId")
                        .HasColumnType("int");

                    b.Property<int>("Profit")
                        .HasColumnType("int");

                    b.Property<int>("TotalPrice")
                        .HasColumnType("int");

                    b.Property<int>("UnUseDay")
                        .HasColumnType("int");

                    b.HasKey("ProductOfferId");

                    b.HasIndex("ProductId");

                    b.ToTable("ProductOffer","Product");
                });

            modelBuilder.Entity("GolPooch.Domain.Entity.Purchase", b =>
                {
                    b.Property<int>("PurchaseId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<byte>("Chance")
                        .HasColumnType("tinyint");

                    b.Property<DateTime>("InsertDateMi")
                        .HasColumnType("datetime2");

                    b.Property<string>("InsertDateSh")
                        .HasColumnType("char(10)")
                        .HasMaxLength(10);

                    b.Property<bool>("IsFinished")
                        .HasColumnType("bit");

                    b.Property<bool>("IsReFoundable")
                        .HasColumnType("bit");

                    b.Property<DateTime>("ModifyDateMi")
                        .HasColumnType("datetime2");

                    b.Property<string>("ModifyDateSh")
                        .HasColumnType("char(10)")
                        .HasMaxLength(10);

                    b.Property<int>("PaymentTransactionId")
                        .HasColumnType("int");

                    b.Property<int>("ProductOfferId")
                        .HasColumnType("int");

                    b.Property<byte>("UsedChance")
                        .HasColumnType("tinyint");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.HasKey("PurchaseId");

                    b.HasIndex("PaymentTransactionId");

                    b.HasIndex("ProductOfferId");

                    b.HasIndex("UserId");

                    b.ToTable("Purchase","Product");
                });

            modelBuilder.Entity("GolPooch.Domain.Entity.Round", b =>
                {
                    b.Property<int>("RoundId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("ChestId")
                        .HasColumnType("int");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(250)")
                        .HasMaxLength(250);

                    b.Property<DateTime>("EndDateMi")
                        .HasColumnType("datetime2");

                    b.Property<string>("EndDateSh")
                        .HasColumnType("char(10)")
                        .HasMaxLength(10);

                    b.Property<DateTime>("InsertDateMi")
                        .HasColumnType("datetime2");

                    b.Property<string>("InsertDateSh")
                        .HasColumnType("char(10)")
                        .HasMaxLength(10);

                    b.Property<DateTime>("ModifyDateMi")
                        .HasColumnType("datetime2");

                    b.Property<string>("ModifyDateSh")
                        .HasColumnType("char(10)")
                        .HasMaxLength(10);

                    b.Property<int>("ParticipantCount")
                        .HasColumnType("int");

                    b.Property<DateTime>("StartDateMi")
                        .HasColumnType("datetime2");

                    b.Property<string>("StartDateSh")
                        .HasColumnType("char(10)")
                        .HasMaxLength(10);

                    b.Property<byte>("State")
                        .HasColumnType("tinyint");

                    b.Property<int>("WinnerUserId")
                        .HasColumnType("int");

                    b.HasKey("RoundId");

                    b.HasIndex("ChestId");

                    b.HasIndex("WinnerUserId");

                    b.ToTable("Round","Draw");
                });

            modelBuilder.Entity("GolPooch.Domain.Entity.Ticket", b =>
                {
                    b.Property<int>("TicketId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Answer")
                        .HasColumnType("nvarchar(1000)")
                        .HasMaxLength(1000);

                    b.Property<Guid?>("AnswerAdminId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("AnswerDateMi")
                        .HasColumnType("datetime2");

                    b.Property<string>("AnswerDateSh")
                        .HasColumnType("char(10)")
                        .HasMaxLength(10);

                    b.Property<DateTime>("InsertDateMi")
                        .HasColumnType("datetime2");

                    b.Property<string>("InsertDateSh")
                        .HasColumnType("char(10)")
                        .HasMaxLength(10);

                    b.Property<bool>("IsRead")
                        .HasColumnType("bit");

                    b.Property<string>("Text")
                        .IsRequired()
                        .HasColumnType("nvarchar(1000)")
                        .HasMaxLength(1000);

                    b.Property<int>("Type")
                        .HasColumnType("int");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.HasKey("TicketId");

                    b.HasIndex("UserId");

                    b.ToTable("Ticket","Base");
                });

            modelBuilder.Entity("GolPooch.Domain.Entity.User", b =>
                {
                    b.Property<int>("UserId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("BirthdateMi")
                        .HasColumnType("datetime2");

                    b.Property<string>("BirthdateSh")
                        .HasColumnType("char(10)")
                        .HasMaxLength(10);

                    b.Property<string>("Email")
                        .HasColumnType("varchar(150)")
                        .HasMaxLength(150);

                    b.Property<string>("FirstName")
                        .HasColumnType("nvarchar(25)")
                        .HasMaxLength(25);

                    b.Property<byte>("Gender")
                        .HasColumnType("tinyint");

                    b.Property<DateTime>("InsertDateMi")
                        .HasColumnType("datetime2");

                    b.Property<string>("InsertDateSh")
                        .HasColumnType("char(10)")
                        .HasMaxLength(10);

                    b.Property<string>("LastName")
                        .HasColumnType("nvarchar(30)")
                        .HasMaxLength(30);

                    b.Property<long>("MobileNumber")
                        .HasColumnType("bigint");

                    b.Property<string>("ProfileImgUrl")
                        .HasColumnType("varchar(250)")
                        .HasMaxLength(250);

                    b.Property<string>("PushId")
                        .HasColumnType("varchar(150)")
                        .HasMaxLength(150);

                    b.Property<int>("Region")
                        .HasColumnType("int");

                    b.HasKey("UserId");

                    b.HasIndex("MobileNumber")
                        .IsUnique();

                    b.ToTable("User","Base");
                });

            modelBuilder.Entity("GolPooch.Domain.Entity.UserDeviceLog", b =>
                {
                    b.Property<int>("UserDeviceLogId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Application")
                        .HasColumnType("varchar(50)")
                        .HasMaxLength(50);

                    b.Property<string>("Device")
                        .HasColumnType("varchar(50)")
                        .HasMaxLength(50);

                    b.Property<string>("IP")
                        .HasColumnType("varchar(20)")
                        .HasMaxLength(20);

                    b.Property<DateTime>("InsertDateMi")
                        .HasColumnType("datetime2");

                    b.Property<string>("InsertDateSh")
                        .HasColumnType("char(10)")
                        .HasMaxLength(10);

                    b.Property<bool>("IsMobile")
                        .HasColumnType("bit");

                    b.Property<long>("MobileNumber")
                        .HasColumnType("bigint");

                    b.Property<string>("Os")
                        .HasColumnType("varchar(20)")
                        .HasMaxLength(20);

                    b.HasKey("UserDeviceLogId");

                    b.ToTable("UserDeviceLog","Base");
                });

            modelBuilder.Entity("GolPooch.Domain.Entity.Banner", b =>
                {
                    b.HasOne("GolPooch.Domain.Entity.Page", "Page")
                        .WithMany()
                        .HasForeignKey("PageId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("GolPooch.Domain.Entity.DrawChance", b =>
                {
                    b.HasOne("GolPooch.Domain.Entity.Purchase", "Purchase")
                        .WithMany("DrawChances")
                        .HasForeignKey("PurchaseId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("GolPooch.Domain.Entity.Round", "Round")
                        .WithMany("DrawChances")
                        .HasForeignKey("RoundId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("GolPooch.Domain.Entity.User", "User")
                        .WithMany("DrawChances")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Restrict);
                });

            modelBuilder.Entity("GolPooch.Domain.Entity.Notification", b =>
                {
                    b.HasOne("GolPooch.Domain.Entity.User", "User")
                        .WithMany()
                        .HasForeignKey("UserId");
                });

            modelBuilder.Entity("GolPooch.Domain.Entity.NotificationDelivery", b =>
                {
                    b.HasOne("GolPooch.Domain.Entity.Notification", "Notification")
                        .WithMany()
                        .HasForeignKey("NotificationId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("GolPooch.Domain.Entity.User", "User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("GolPooch.Domain.Entity.PaymentTransaction", b =>
                {
                    b.HasOne("GolPooch.Domain.Entity.PaymentGateway", "PaymentGateway")
                        .WithMany("PaymentTransactions")
                        .HasForeignKey("PaymentGatewayId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("GolPooch.Domain.Entity.ProductOffer", "ProductOffer")
                        .WithMany("PaymentTransactions")
                        .HasForeignKey("ProductOfferId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("GolPooch.Domain.Entity.User", "User")
                        .WithMany("PaymentTransactions")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();
                });

            modelBuilder.Entity("GolPooch.Domain.Entity.ProductOffer", b =>
                {
                    b.HasOne("GolPooch.Domain.Entity.Product", "Product")
                        .WithMany("ProductOffers")
                        .HasForeignKey("ProductId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();
                });

            modelBuilder.Entity("GolPooch.Domain.Entity.Purchase", b =>
                {
                    b.HasOne("GolPooch.Domain.Entity.PaymentTransaction", "PaymentTransaction")
                        .WithMany("Purchases")
                        .HasForeignKey("PaymentTransactionId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("GolPooch.Domain.Entity.ProductOffer", "ProductOffer")
                        .WithMany("Purchases")
                        .HasForeignKey("ProductOfferId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("GolPooch.Domain.Entity.User", "User")
                        .WithMany("Purchases")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();
                });

            modelBuilder.Entity("GolPooch.Domain.Entity.Round", b =>
                {
                    b.HasOne("GolPooch.Domain.Entity.Chest", "Chest")
                        .WithMany("Rounds")
                        .HasForeignKey("ChestId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("GolPooch.Domain.Entity.User", "WinnerUser")
                        .WithMany("Rounds")
                        .HasForeignKey("WinnerUserId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();
                });

            modelBuilder.Entity("GolPooch.Domain.Entity.Ticket", b =>
                {
                    b.HasOne("GolPooch.Domain.Entity.User", "User")
                        .WithMany("Tickets")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
