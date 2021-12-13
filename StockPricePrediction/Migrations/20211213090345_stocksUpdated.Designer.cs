﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using RepositoryLayer;

namespace StockPricePrediction.Migrations
{
    [DbContext(typeof(AppDbContext))]
    [Migration("20211213090345_stocksUpdated")]
    partial class stocksUpdated
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 63)
                .HasAnnotation("ProductVersion", "5.0.3")
                .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

            modelBuilder.Entity("DomainLayer.Comment", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INT")
                        .HasColumnName("id")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<string>("Author")
                        .IsRequired()
                        .HasColumnType("VARCHAR(100)")
                        .HasColumnName("author");

                    b.Property<DateTime>("CreationDate")
                        .HasColumnType("timestamp without time zone");

                    b.Property<int>("Dislikes")
                        .HasColumnType("integer");

                    b.Property<int>("Likes")
                        .HasColumnType("integer");

                    b.Property<string>("Message")
                        .IsRequired()
                        .HasColumnType("VARCHAR(100)")
                        .HasColumnName("message");

                    b.Property<int?>("StockId")
                        .HasColumnType("INT");

                    b.HasKey("Id")
                        .HasName("pk_commentid");

                    b.HasIndex("StockId");

                    b.ToTable("Comment");
                });

            modelBuilder.Entity("DomainLayer.Stock", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INT")
                        .HasColumnName("id")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<string>("Symbol")
                        .IsRequired()
                        .HasColumnType("VARCHAR(100)")
                        .HasColumnName("symbol");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("VARCHAR(100)")
                        .HasColumnName("title");

                    b.HasKey("Id")
                        .HasName("pk_stockid");

                    b.ToTable("Stock");
                });

            modelBuilder.Entity("DomainLayer.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INT")
                        .HasColumnName("id")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<DateTime>("CreationDate")
                        .HasColumnType("timestamp without time zone");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("VARCHAR(50)")
                        .HasColumnName("email");

                    b.Property<string>("Firstname")
                        .IsRequired()
                        .HasColumnType("VARCHAR(100)")
                        .HasColumnName("firstname");

                    b.Property<string>("Lastname")
                        .IsRequired()
                        .HasColumnType("VARCHAR(100)")
                        .HasColumnName("lastname");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("VARCHAR(50)")
                        .HasColumnName("password");

                    b.HasKey("Id")
                        .HasName("pk_userid");

                    b.ToTable("User");
                });

            modelBuilder.Entity("DomainLayer.UserStocks", b =>
                {
                    b.Property<int>("UserId")
                        .HasColumnType("INT");

                    b.Property<int>("StockId")
                        .HasColumnType("INT");

                    b.HasKey("UserId", "StockId");

                    b.HasIndex("StockId");

                    b.ToTable("UserStocks");
                });

            modelBuilder.Entity("DomainLayer.Comment", b =>
                {
                    b.HasOne("DomainLayer.Stock", null)
                        .WithMany("Comments")
                        .HasForeignKey("StockId");
                });

            modelBuilder.Entity("DomainLayer.UserStocks", b =>
                {
                    b.HasOne("DomainLayer.Stock", "Stock")
                        .WithMany("UsersStocks")
                        .HasForeignKey("StockId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("DomainLayer.User", "User")
                        .WithMany("UserStocks")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Stock");

                    b.Navigation("User");
                });

            modelBuilder.Entity("DomainLayer.Stock", b =>
                {
                    b.Navigation("Comments");

                    b.Navigation("UsersStocks");
                });

            modelBuilder.Entity("DomainLayer.User", b =>
                {
                    b.Navigation("UserStocks");
                });
#pragma warning restore 612, 618
        }
    }
}
