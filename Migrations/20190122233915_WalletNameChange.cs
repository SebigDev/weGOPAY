using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace weGOPAY.Migrations
{
    public partial class WalletNameChange : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Account");

            migrationBuilder.CreateTable(
                name: "Wallet",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    UserId = table.Column<string>(nullable: true),
                    Currency = table.Column<int>(nullable: false),
                    NairaBalance = table.Column<decimal>(nullable: false),
                    USDBalance = table.Column<decimal>(nullable: false),
                    EuroBalance = table.Column<decimal>(nullable: false),
                    PoundBalance = table.Column<decimal>(nullable: false),
                    YenBalance = table.Column<decimal>(nullable: false),
                    Status = table.Column<string>(nullable: true),
                    WalletCreationDate = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Wallet", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Wallet");

            migrationBuilder.CreateTable(
                name: "Account",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    AccountCreationDate = table.Column<DateTime>(nullable: false),
                    Amount = table.Column<decimal>(nullable: false),
                    Currency = table.Column<int>(nullable: false),
                    EuroBalance = table.Column<decimal>(nullable: false),
                    NairaBalance = table.Column<decimal>(nullable: false),
                    PoundBalance = table.Column<decimal>(nullable: false),
                    Status = table.Column<string>(nullable: true),
                    USDBalance = table.Column<decimal>(nullable: false),
                    UserId = table.Column<string>(nullable: true),
                    YenBalance = table.Column<decimal>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Account", x => x.Id);
                });
        }
    }
}
