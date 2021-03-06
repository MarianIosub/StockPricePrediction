// <auto-generated />
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
    [Migration("20211204193212_AddedFavouriteStocks_2")]
    partial class AddedFavouriteStocks_2
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

                    b.Property<int?>("UserId")
                        .HasColumnType("INT");

                    b.HasKey("Id")
                        .HasName("pk_stockid");

                    b.HasIndex("UserId");

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

            modelBuilder.Entity("DomainLayer.Comment", b =>
                {
                    b.HasOne("DomainLayer.Stock", null)
                        .WithMany("Comments")
                        .HasForeignKey("StockId");
                });

            modelBuilder.Entity("DomainLayer.Stock", b =>
                {
                    b.HasOne("DomainLayer.User", null)
                        .WithMany("FavouriteStocks")
                        .HasForeignKey("UserId");
                });

            modelBuilder.Entity("DomainLayer.Stock", b =>
                {
                    b.Navigation("Comments");
                });

            modelBuilder.Entity("DomainLayer.User", b =>
                {
                    b.Navigation("FavouriteStocks");
                });
#pragma warning restore 612, 618
        }
    }
}
