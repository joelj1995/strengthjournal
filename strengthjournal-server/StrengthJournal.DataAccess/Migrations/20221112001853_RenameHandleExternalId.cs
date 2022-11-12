using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace StrengthJournal.DataAccess.Migrations
{
    public partial class RenameHandleExternalId : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Handle",
                table: "Users",
                newName: "ExternalId");

            migrationBuilder.RenameIndex(
                name: "IX_Users_Handle",
                table: "Users",
                newName: "IX_Users_ExternalId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ExternalId",
                table: "Users",
                newName: "Handle");

            migrationBuilder.RenameIndex(
                name: "IX_Users_ExternalId",
                table: "Users",
                newName: "IX_Users_Handle");
        }
    }
}
