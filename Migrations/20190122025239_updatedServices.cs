using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace weGOPAY.Migrations
{
    public partial class updatedServices : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AccountCreditRequestMap");

            migrationBuilder.DropTable(
                name: "AccountDebitRequestsMap");

            migrationBuilder.DropTable(
                name: "CreditRequest");

            migrationBuilder.DropTable(
                name: "DebitRequest");

            migrationBuilder.CreateTable(
                name: "TransactionRequest",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    UserId = table.Column<string>(nullable: true),
                    ReqCurrency = table.Column<int>(nullable: false),
                    ResCurrency = table.Column<int>(nullable: false),
                    TransactionType = table.Column<int>(nullable: false),
                    Amount = table.Column<decimal>(nullable: false),
                    CurrentBalance = table.Column<decimal>(nullable: false),
                    RequestStatus = table.Column<int>(nullable: false),
                    CreditRequestDate = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TransactionRequest", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TransactionRequest");

            migrationBuilder.CreateTable(
                name: "AccountCreditRequestMap",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    AccountId = table.Column<long>(nullable: false),
                    CreditAmount = table.Column<decimal>(nullable: false),
                    CreditRequestId = table.Column<long>(nullable: false),
                    Currency = table.Column<int>(nullable: false),
                    DateMapped = table.Column<DateTime>(nullable: false),
                    PreviousBalance = table.Column<decimal>(nullable: false),
                    UserId = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AccountCreditRequestMap", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AccountDebitRequestsMap",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    AccountId = table.Column<long>(nullable: false),
                    Currency = table.Column<int>(nullable: false),
                    DateMapped = table.Column<DateTime>(nullable: false),
                    DebitAmount = table.Column<decimal>(nullable: false),
                    DebitRequestId = table.Column<long>(nullable: false),
                    PreviousBalance = table.Column<decimal>(nullable: false),
                    UserId = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AccountDebitRequestsMap", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CreditRequest",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CreditAmount = table.Column<double>(nullable: false),
                    CreditRequestDate = table.Column<DateTime>(nullable: false),
                    Currency = table.Column<string>(nullable: true),
                    CurrentBalance = table.Column<decimal>(nullable: false),
                    RequestStatus = table.Column<string>(nullable: true),
                    UserId = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CreditRequest", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "DebitRequest",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Currency = table.Column<string>(nullable: true),
                    CurrentBalance = table.Column<decimal>(nullable: false),
                    DebitAmount = table.Column<double>(nullable: false),
                    DebitRequestDate = table.Column<DateTime>(nullable: false),
                    RequestStatus = table.Column<string>(nullable: true),
                    UserId = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DebitRequest", x => x.Id);
                });
        }
    }
}
