using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PriceTagPrinter.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Goods",
                columns: table => new
                {
                    GoodsCode = table.Column<string>(type: "TEXT", nullable: false),
                    GoodsName = table.Column<string>(type: "TEXT", nullable: false),
                    GoodsPrice = table.Column<int>(type: "INTEGER", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Goods", x => x.GoodsCode);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Goods");
        }
    }
}
