using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace CoinTracker.Data.Migrations
{
    public partial class Coin : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Coin",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Exchange = table.Column<string>(nullable: true),
                    Pair = table.Column<string>(nullable: true),
                    CurrentPrice = table.Column<decimal>(type: "decimal(18,8)", nullable: false),
                    PriceChange = table.Column<decimal>(type: "decimal(18,8)", nullable: false),
                    Volume = table.Column<decimal>(type: "decimal(18,8)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Coin", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Coin");
        }
    }
}
