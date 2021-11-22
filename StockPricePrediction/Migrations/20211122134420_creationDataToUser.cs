using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace StockPricePrediction.Migrations
{
    public partial class creationDataToUser : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Stock_User_UserId",
                table: "Stock");

            migrationBuilder.DropIndex(
                name: "IX_Stock_UserId",
                table: "Stock");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Stock");

            migrationBuilder.AddColumn<DateTime>(
                name: "CreationDate",
                table: "User",
                type: "timestamp without time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CreationDate",
                table: "User");

            migrationBuilder.AddColumn<int>(
                name: "UserId",
                table: "Stock",
                type: "INT",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Stock_UserId",
                table: "Stock",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Stock_User_UserId",
                table: "Stock",
                column: "UserId",
                principalTable: "User",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
