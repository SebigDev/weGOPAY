using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace weGOPAY.Migrations
{
    public partial class Remodelling : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TransactionRequest");

            migrationBuilder.DropColumn(
                name: "Currency",
                table: "Wallet");

            migrationBuilder.CreateTable(
                name: "WalletTransaction",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    UserId = table.Column<string>(nullable: true),
                    RequestCurrency = table.Column<int>(nullable: false),
                    Amount = table.Column<decimal>(nullable: false),
                    ResponseCurrency = table.Column<int>(nullable: false),
                    TransactionStatus = table.Column<int>(nullable: false),
                    TransactionDate = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WalletTransaction", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "WalletTransaction");

            migrationBuilder.AddColumn<int>(
                name: "Currency",
                table: "Wallet",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "TransactionRequest",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Amount = table.Column<decimal>(nullable: false),
                    CreditRequestDate = table.Column<DateTime>(nullable: false),
                    CurrentBalance = table.Column<decimal>(nullable: false),
                    ReqCurrency = table.Column<int>(nullable: false),
                    RequestStatus = table.Column<int>(nullable: false),
                    ResCurrency = table.Column<int>(nullable: false),
                    TransactionType = table.Column<int>(nullable: false),
                    UserId = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TransactionRequest", x => x.Id);
                });
        }
    }
}
