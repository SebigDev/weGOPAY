using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace weGOPAY.Migrations
{
    public partial class Init : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Account",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    UserId = table.Column<string>(nullable: true),
                    Currency = table.Column<int>(nullable: false),
                    Amount = table.Column<decimal>(nullable: false),
                    Status = table.Column<string>(nullable: true),
                    AccountCreationDate = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Account", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AccountCreditRequestMap",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    UserId = table.Column<long>(nullable: false),
                    AccountId = table.Column<long>(nullable: false),
                    CreditRequestId = table.Column<long>(nullable: false),
                    Currency = table.Column<int>(nullable: false),
                    CreditAmount = table.Column<decimal>(nullable: false),
                    PreviousBalance = table.Column<decimal>(nullable: false),
                    DateMapped = table.Column<DateTime>(nullable: false)
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
                    UserId = table.Column<long>(nullable: false),
                    AccountId = table.Column<long>(nullable: false),
                    DebitRequestId = table.Column<long>(nullable: false),
                    Currency = table.Column<int>(nullable: false),
                    DebitAmount = table.Column<decimal>(nullable: false),
                    PreviousBalance = table.Column<decimal>(nullable: false),
                    DateMapped = table.Column<DateTime>(nullable: false)
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
                    UserId = table.Column<string>(nullable: true),
                    Currency = table.Column<string>(nullable: true),
                    CreditAmount = table.Column<double>(nullable: false),
                    CurrentBalance = table.Column<decimal>(nullable: false),
                    RequestStatus = table.Column<string>(nullable: true),
                    CreditRequestDate = table.Column<DateTime>(nullable: false)
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
                    UserId = table.Column<string>(nullable: true),
                    Currency = table.Column<string>(nullable: true),
                    DebitAmount = table.Column<double>(nullable: false),
                    CurrentBalance = table.Column<decimal>(nullable: false),
                    RequestStatus = table.Column<string>(nullable: true),
                    DebitRequestDate = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DebitRequest", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "User",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    UserId = table.Column<string>(nullable: true),
                    Fullname = table.Column<string>(nullable: true),
                    EmailAddress = table.Column<string>(nullable: true),
                    PasswordHash = table.Column<byte[]>(nullable: true),
                    PasswordSalt = table.Column<byte[]>(nullable: true),
                    CountryOfOrigin = table.Column<string>(nullable: true),
                    CountryOfResidence = table.Column<string>(nullable: true),
                    Gender = table.Column<string>(nullable: true),
                    Status = table.Column<string>(nullable: true),
                    DateRegistered = table.Column<DateTime>(nullable: false),
                    DateUpdated = table.Column<DateTime>(nullable: false),
                    IsUpdated = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_User", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Account");

            migrationBuilder.DropTable(
                name: "AccountCreditRequestMap");

            migrationBuilder.DropTable(
                name: "AccountDebitRequestsMap");

            migrationBuilder.DropTable(
                name: "CreditRequest");

            migrationBuilder.DropTable(
                name: "DebitRequest");

            migrationBuilder.DropTable(
                name: "User");
        }
    }
}
