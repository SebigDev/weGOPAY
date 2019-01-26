using Microsoft.EntityFrameworkCore.Migrations;

namespace weGOPAY.Migrations
{
    public partial class Init1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "EuroBalance",
                table: "Account",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "NairaBalance",
                table: "Account",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "PoundBalance",
                table: "Account",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "USDBalance",
                table: "Account",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "YenBalance",
                table: "Account",
                nullable: false,
                defaultValue: 0m);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "EuroBalance",
                table: "Account");

            migrationBuilder.DropColumn(
                name: "NairaBalance",
                table: "Account");

            migrationBuilder.DropColumn(
                name: "PoundBalance",
                table: "Account");

            migrationBuilder.DropColumn(
                name: "USDBalance",
                table: "Account");

            migrationBuilder.DropColumn(
                name: "YenBalance",
                table: "Account");
        }
    }
}
