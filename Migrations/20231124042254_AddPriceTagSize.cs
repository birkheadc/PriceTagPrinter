using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PriceTagPrinter.Migrations
{
    public partial class AddPriceTagSize : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Size",
                table: "PriceTags",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Size",
                table: "PriceTags");
        }
    }
}
