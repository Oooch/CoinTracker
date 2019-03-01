using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace CoinTracker.Data.Migrations
{
    public partial class LastUpdated : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<decimal>(
                name: "Volume",
                table: "Coin",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,8)");

            migrationBuilder.AlterColumn<decimal>(
                name: "PriceChange",
                table: "Coin",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,8)");

            migrationBuilder.AlterColumn<decimal>(
                name: "CurrentPrice",
                table: "Coin",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,8)");

            migrationBuilder.AddColumn<DateTime>(
                name: "LastUpdated",
                table: "Coin",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "LastUpdated",
                table: "Coin");

            migrationBuilder.AlterColumn<decimal>(
                name: "Volume",
                table: "Coin",
                type: "decimal(18,8)",
                nullable: false,
                oldClrType: typeof(decimal));

            migrationBuilder.AlterColumn<decimal>(
                name: "PriceChange",
                table: "Coin",
                type: "decimal(18,8)",
                nullable: false,
                oldClrType: typeof(decimal));

            migrationBuilder.AlterColumn<decimal>(
                name: "CurrentPrice",
                table: "Coin",
                type: "decimal(18,8)",
                nullable: false,
                oldClrType: typeof(decimal));
        }
    }
}
