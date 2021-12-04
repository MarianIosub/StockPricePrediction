using Microsoft.EntityFrameworkCore.Migrations;

namespace StockPricePrediction.Migrations
{
    public partial class AddedFavouriteStocks : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "pk_id",
                table: "User");

            migrationBuilder.AddColumn<int>(
                name: "UserId",
                table: "Stock",
                type: "INT",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "pk_userid",
                table: "User",
                column: "id");

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

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Stock_User_UserId",
                table: "Stock");

            migrationBuilder.DropPrimaryKey(
                name: "pk_userid",
                table: "User");

            migrationBuilder.DropIndex(
                name: "IX_Stock_UserId",
                table: "Stock");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Stock");

            migrationBuilder.AddPrimaryKey(
                name: "pk_id",
                table: "User",
                column: "id");
        }
    }
}
