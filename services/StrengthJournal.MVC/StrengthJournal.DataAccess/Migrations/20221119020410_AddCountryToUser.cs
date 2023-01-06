using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace StrengthJournal.DataAccess.Migrations
{
    public partial class AddCountryToUser : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "UserCountryCode",
                table: "Users",
                type: "nvarchar(2)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Users_UserCountryCode",
                table: "Users",
                column: "UserCountryCode");

            migrationBuilder.AddForeignKey(
                name: "FK_Users_Countries_UserCountryCode",
                table: "Users",
                column: "UserCountryCode",
                principalTable: "Countries",
                principalColumn: "Code");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Users_Countries_UserCountryCode",
                table: "Users");

            migrationBuilder.DropIndex(
                name: "IX_Users_UserCountryCode",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "UserCountryCode",
                table: "Users");
        }
    }
}
