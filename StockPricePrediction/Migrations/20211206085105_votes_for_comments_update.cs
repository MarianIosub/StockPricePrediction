using Microsoft.EntityFrameworkCore.Migrations;

namespace StockPricePrediction.Migrations
{
    public partial class votes_for_comments_update : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Likes1",
                table: "Comment",
                newName: "Likes");

            migrationBuilder.RenameColumn(
                name: "Dislikes1",
                table: "Comment",
                newName: "Dislikes");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Likes",
                table: "Comment",
                newName: "Likes1");

            migrationBuilder.RenameColumn(
                name: "Dislikes",
                table: "Comment",
                newName: "Dislikes1");
        }
    }
}
