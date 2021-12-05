using Microsoft.EntityFrameworkCore.Migrations;

namespace StockPricePrediction.Migrations
{
    public partial class favouriteStockUpdateRelation : Migration
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

            migrationBuilder.CreateTable(
                name: "UserStocks",
                columns: table => new
                {
                    UserId = table.Column<int>(type: "INT", nullable: false),
                    StockId = table.Column<int>(type: "INT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserStocks", x => new { x.UserId, x.StockId });
                    table.ForeignKey(
                        name: "FK_UserStocks_Stock_StockId",
                        column: x => x.StockId,
                        principalTable: "Stock",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserStocks_User_UserId",
                        column: x => x.UserId,
                        principalTable: "User",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_UserStocks_StockId",
                table: "UserStocks",
                column: "StockId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "UserStocks");

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
