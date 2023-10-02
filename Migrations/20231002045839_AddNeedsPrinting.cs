using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PriceTagPrinter.Migrations
{
    public partial class AddNeedsPrinting : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "NeedsPrinting",
                table: "PriceTags",
                type: "INTEGER",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "NeedsPrinting",
                table: "PriceTags");
        }
    }
}
