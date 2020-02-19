using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace BuyStockAdviser.Migrations
{
    public partial class Buy : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "StockItems",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Datetime = table.Column<DateTime>(nullable: false),
                    Symbol = table.Column<string>(nullable: true),
                    Open = table.Column<double>(nullable: false),
                    High = table.Column<double>(nullable: false),
                    Last = table.Column<double>(nullable: false),
                    Volume = table.Column<double>(nullable: false),
                    Decision = table.Column<string>(nullable: true),
                    SlopeLeft = table.Column<double>(nullable: false),
                    SlopeRight = table.Column<double>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StockItems", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "StockSymbols",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Symbol = table.Column<string>(nullable: true),
                    Name = table.Column<string>(nullable: true),
                    Market = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StockSymbols", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "StockItems");

            migrationBuilder.DropTable(
                name: "StockSymbols");
        }
    }
}
