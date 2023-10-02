using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PriceTagPrinter.Migrations
{
    public partial class FixTableName : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Goods",
                table: "Goods");

            migrationBuilder.RenameTable(
                name: "Goods",
                newName: "PriceTags");

            migrationBuilder.AddPrimaryKey(
                name: "PK_PriceTags",
                table: "PriceTags",
                column: "GoodsCode");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_PriceTags",
                table: "PriceTags");

            migrationBuilder.RenameTable(
                name: "PriceTags",
                newName: "Goods");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Goods",
                table: "Goods",
                column: "GoodsCode");
        }
    }
}
