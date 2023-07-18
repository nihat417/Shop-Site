﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Shop_Site.Data;

#nullable disable

namespace Shop_Site.Migrations
{
    [DbContext(typeof(AppDbContext))]
    [Migration("20230718173812_mig2")]
    partial class mig2
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.9")
                .HasAnnotation("Proxies:ChangeTracking", false)
                .HasAnnotation("Proxies:CheckEquality", false)
                .HasAnnotation("Proxies:LazyLoading", true)
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("Shop_Site.Models.Brand", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Brands");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            CreatedDate = new DateTime(2023, 7, 18, 21, 38, 12, 392, DateTimeKind.Local).AddTicks(2820),
                            Name = "SUPREME"
                        },
                        new
                        {
                            Id = 2,
                            CreatedDate = new DateTime(2023, 7, 18, 21, 38, 12, 392, DateTimeKind.Local).AddTicks(2828),
                            Name = "OFF-WHITE"
                        },
                        new
                        {
                            Id = 3,
                            CreatedDate = new DateTime(2023, 7, 18, 21, 38, 12, 392, DateTimeKind.Local).AddTicks(2829),
                            Name = "STUSSY"
                        },
                        new
                        {
                            Id = 4,
                            CreatedDate = new DateTime(2023, 7, 18, 21, 38, 12, 392, DateTimeKind.Local).AddTicks(2830),
                            Name = "VETEMENTS"
                        });
                });

            modelBuilder.Entity("Shop_Site.Models.Category", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Categories");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            CreatedDate = new DateTime(2023, 7, 18, 21, 38, 12, 392, DateTimeKind.Local).AddTicks(2905),
                            Name = "Bloomers"
                        },
                        new
                        {
                            Id = 2,
                            CreatedDate = new DateTime(2023, 7, 18, 21, 38, 12, 392, DateTimeKind.Local).AddTicks(2905),
                            Name = "Blouse"
                        },
                        new
                        {
                            Id = 3,
                            CreatedDate = new DateTime(2023, 7, 18, 21, 38, 12, 392, DateTimeKind.Local).AddTicks(2906),
                            Name = "Bodysuit"
                        },
                        new
                        {
                            Id = 4,
                            CreatedDate = new DateTime(2023, 7, 18, 21, 38, 12, 392, DateTimeKind.Local).AddTicks(2907),
                            Name = "Coat"
                        });
                });

            modelBuilder.Entity("Shop_Site.Models.Products", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("BrandId")
                        .HasColumnType("int");

                    b.Property<int>("CategoryId")
                        .HasColumnType("int");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ImageUrl")
                        .HasColumnType("nvarchar(max)");

                    b.Property<decimal>("Price")
                        .HasColumnType("decimal(18,2)");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("BrandId");

                    b.HasIndex("CategoryId");

                    b.ToTable("Products");
                });

            modelBuilder.Entity("Shop_Site.Models.Products", b =>
                {
                    b.HasOne("Shop_Site.Models.Brand", "brand")
                        .WithMany("Product")
                        .HasForeignKey("BrandId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Shop_Site.Models.Category", "category")
                        .WithMany("Product")
                        .HasForeignKey("CategoryId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("brand");

                    b.Navigation("category");
                });

            modelBuilder.Entity("Shop_Site.Models.Brand", b =>
                {
                    b.Navigation("Product");
                });

            modelBuilder.Entity("Shop_Site.Models.Category", b =>
                {
                    b.Navigation("Product");
                });
#pragma warning restore 612, 618
        }
    }
}
