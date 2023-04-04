﻿// <auto-generated />
using System;
using BPJ_U2T.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace BPJU2T.Migrations
{
    [DbContext(typeof(DatabaseContext))]
    [Migration("20230403083644_InitialCreate")]
    partial class InitialCreate
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.1")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("BPJ_U2T.Models.Account", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ID"));

                    b.Property<string>("Address")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Image")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PhoneNumber")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("RoleID")
                        .HasColumnType("int");

                    b.HasKey("ID");

                    b.HasIndex("RoleID");

                    b.ToTable("Account");
                });

            modelBuilder.Entity("BPJ_U2T.Models.Cart", b =>
                {
                    b.Property<string>("ID")
                        .HasColumnType("nvarchar(450)");

                    b.Property<int>("AccountID")
                        .HasColumnType("int");

                    b.Property<int>("AmountProduct")
                        .HasColumnType("int");

                    b.Property<DateTime>("Created")
                        .HasColumnType("datetime2");

                    b.Property<int>("ProductID")
                        .HasColumnType("int");

                    b.HasKey("ID");

                    b.HasIndex("AccountID");

                    b.HasIndex("ProductID");

                    b.ToTable("Cart");
                });

            modelBuilder.Entity("BPJ_U2T.Models.CategoryProduct", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("CategoryProducts");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Name = "ของกิน"
                        },
                        new
                        {
                            Id = 2,
                            Name = "ของใช้"
                        });
                });

            modelBuilder.Entity("BPJ_U2T.Models.District", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ID"));

                    b.Property<string>("Image")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("TextDistrict")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("ID");

                    b.ToTable("Districts");
                });

            modelBuilder.Entity("BPJ_U2T.Models.OrderAggregate.OrderAccount", b =>
                {
                    b.Property<string>("ID")
                        .HasColumnType("nvarchar(450)");

                    b.Property<int>("AccountID")
                        .HasColumnType("int");

                    b.Property<bool>("AccountStatus")
                        .HasColumnType("bit");

                    b.Property<DateTime>("Created")
                        .HasColumnType("datetime2");

                    b.Property<int>("DeliveryFee")
                        .HasColumnType("int");

                    b.Property<int>("PaymentStatus")
                        .HasColumnType("int");

                    b.Property<int>("PriceTotal")
                        .HasColumnType("int");

                    b.Property<string>("ProofOfPayment")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("ID");

                    b.HasIndex("AccountID");

                    b.ToTable("OrderAccounts");
                });

            modelBuilder.Entity("BPJ_U2T.Models.OrderAggregate.OrderItem", b =>
                {
                    b.Property<string>("ID")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("OrderAccountID")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<int>("ProductAmount")
                        .HasColumnType("int");

                    b.Property<int>("ProductID")
                        .HasColumnType("int");

                    b.Property<int>("ProductPrice")
                        .HasColumnType("int");

                    b.HasKey("ID");

                    b.HasIndex("OrderAccountID");

                    b.HasIndex("ProductID");

                    b.ToTable("OrderItems");
                });

            modelBuilder.Entity("BPJ_U2T.Models.Product", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ID"));

                    b.Property<int>("CategoryProductID")
                        .HasColumnType("int");

                    b.Property<string>("Detailsinfo")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("DistrictID")
                        .HasColumnType("int");

                    b.Property<string>("Image")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<double>("Price")
                        .HasColumnType("float");

                    b.Property<int>("Stock")
                        .HasColumnType("int");

                    b.HasKey("ID");

                    b.HasIndex("CategoryProductID");

                    b.HasIndex("DistrictID");

                    b.ToTable("Products");
                });

            modelBuilder.Entity("BPJ_U2T.Models.ProductDescription", b =>
                {
                    b.Property<string>("ID")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Image")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("ProductID")
                        .IsRequired()
                        .HasColumnType("int");

                    b.HasKey("ID");

                    b.HasIndex("ProductID");

                    b.ToTable("ProductDescriptions");
                });

            modelBuilder.Entity("BPJ_U2T.Models.ProductDetailProcess", b =>
                {
                    b.Property<string>("ID")
                        .HasColumnType("nvarchar(450)");

                    b.Property<DateTime>("CreatedProductDetail")
                        .HasColumnType("datetime2");

                    b.Property<string>("MakeProductsprocess")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("NameRawMaterial")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("ProductID")
                        .IsRequired()
                        .HasColumnType("int");

                    b.Property<string>("VideoProduct")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("ID");

                    b.HasIndex("ProductID");

                    b.ToTable("ProductDetailProcesses");
                });

            modelBuilder.Entity("BPJ_U2T.Models.Review", b =>
                {
                    b.Property<string>("ID")
                        .HasColumnType("nvarchar(450)");

                    b.Property<int>("AccountID")
                        .HasColumnType("int");

                    b.Property<DateTime>("Created")
                        .HasColumnType("datetime2");

                    b.Property<int?>("ProductID")
                        .IsRequired()
                        .HasColumnType("int");

                    b.Property<string>("Text")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("ID");

                    b.HasIndex("AccountID");

                    b.HasIndex("ProductID");

                    b.ToTable("Reviews");
                });

            modelBuilder.Entity("BPJ_U2T.Models.ReviewImage", b =>
                {
                    b.Property<string>("ID")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Image")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ReviewID")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("ID");

                    b.HasIndex("ReviewID");

                    b.ToTable("ReviewImages");
                });

            modelBuilder.Entity("BPJ_U2T.Models.Role", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ID"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("ID");

                    b.ToTable("Role");

                    b.HasData(
                        new
                        {
                            ID = 1,
                            Name = "user"
                        },
                        new
                        {
                            ID = 2,
                            Name = "admin"
                        });
                });

            modelBuilder.Entity("BPJ_U2T.Models.Account", b =>
                {
                    b.HasOne("BPJ_U2T.Models.Role", "Role")
                        .WithMany()
                        .HasForeignKey("RoleID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Role");
                });

            modelBuilder.Entity("BPJ_U2T.Models.Cart", b =>
                {
                    b.HasOne("BPJ_U2T.Models.Account", "Account")
                        .WithMany()
                        .HasForeignKey("AccountID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("BPJ_U2T.Models.Product", "Product")
                        .WithMany()
                        .HasForeignKey("ProductID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Account");

                    b.Navigation("Product");
                });

            modelBuilder.Entity("BPJ_U2T.Models.OrderAggregate.OrderAccount", b =>
                {
                    b.HasOne("BPJ_U2T.Models.Account", "Account")
                        .WithMany()
                        .HasForeignKey("AccountID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Account");
                });

            modelBuilder.Entity("BPJ_U2T.Models.OrderAggregate.OrderItem", b =>
                {
                    b.HasOne("BPJ_U2T.Models.OrderAggregate.OrderAccount", "OrderAccount")
                        .WithMany()
                        .HasForeignKey("OrderAccountID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("BPJ_U2T.Models.Product", "Product")
                        .WithMany()
                        .HasForeignKey("ProductID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("OrderAccount");

                    b.Navigation("Product");
                });

            modelBuilder.Entity("BPJ_U2T.Models.Product", b =>
                {
                    b.HasOne("BPJ_U2T.Models.CategoryProduct", "CategoryProduct")
                        .WithMany()
                        .HasForeignKey("CategoryProductID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("BPJ_U2T.Models.District", "District")
                        .WithMany()
                        .HasForeignKey("DistrictID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("CategoryProduct");

                    b.Navigation("District");
                });

            modelBuilder.Entity("BPJ_U2T.Models.ProductDescription", b =>
                {
                    b.HasOne("BPJ_U2T.Models.Product", "Product")
                        .WithMany()
                        .HasForeignKey("ProductID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Product");
                });

            modelBuilder.Entity("BPJ_U2T.Models.ProductDetailProcess", b =>
                {
                    b.HasOne("BPJ_U2T.Models.Product", "Product")
                        .WithMany()
                        .HasForeignKey("ProductID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Product");
                });

            modelBuilder.Entity("BPJ_U2T.Models.Review", b =>
                {
                    b.HasOne("BPJ_U2T.Models.Account", "Account")
                        .WithMany()
                        .HasForeignKey("AccountID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("BPJ_U2T.Models.Product", "Product")
                        .WithMany()
                        .HasForeignKey("ProductID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Account");

                    b.Navigation("Product");
                });

            modelBuilder.Entity("BPJ_U2T.Models.ReviewImage", b =>
                {
                    b.HasOne("BPJ_U2T.Models.Review", "Review")
                        .WithMany()
                        .HasForeignKey("ReviewID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Review");
                });
#pragma warning restore 612, 618
        }
    }
}
