using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace StrengthJournal.DataAccess.Migrations
{
    public partial class ExtendWorkoutEntity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "BodyWeightPIT",
                table: "WorkoutLogEntries",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Notes",
                table: "WorkoutLogEntries",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Title",
                table: "WorkoutLogEntries",
                type: "nvarchar(255)",
                maxLength: 255,
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "BodyWeightPIT",
                table: "WorkoutLogEntries");

            migrationBuilder.DropColumn(
                name: "Notes",
                table: "WorkoutLogEntries");

            migrationBuilder.DropColumn(
                name: "Title",
                table: "WorkoutLogEntries");
        }
    }
}
