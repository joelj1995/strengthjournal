using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace StrengthJournal.DataAccess.Migrations
{
    public partial class SimplifyWeightTracking : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "WeightKg",
                table: "WorkoutLogEntrySets");

            migrationBuilder.RenameColumn(
                name: "WeightLbs",
                table: "WorkoutLogEntrySets",
                newName: "Weight");

            migrationBuilder.AddColumn<Guid>(
                name: "WeightUnitId",
                table: "WorkoutLogEntrySets",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "WeightUnits",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FullName = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    Abbreviation = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WeightUnits", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "WeightUnits",
                columns: new[] { "Id", "Abbreviation", "FullName" },
                values: new object[] { new Guid("4bc96550-f274-4a90-978b-92a398f8c49d"), "kg", "Kilograms" });

            migrationBuilder.InsertData(
                table: "WeightUnits",
                columns: new[] { "Id", "Abbreviation", "FullName" },
                values: new object[] { new Guid("bf8df35b-2f45-4a79-a49c-d3aca4a12cd6"), "lbs", "Pounds" });

            migrationBuilder.CreateIndex(
                name: "IX_WorkoutLogEntrySets_WeightUnitId",
                table: "WorkoutLogEntrySets",
                column: "WeightUnitId");

            migrationBuilder.AddForeignKey(
                name: "FK_WorkoutLogEntrySets_WeightUnits_WeightUnitId",
                table: "WorkoutLogEntrySets",
                column: "WeightUnitId",
                principalTable: "WeightUnits",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_WorkoutLogEntrySets_WeightUnits_WeightUnitId",
                table: "WorkoutLogEntrySets");

            migrationBuilder.DropTable(
                name: "WeightUnits");

            migrationBuilder.DropIndex(
                name: "IX_WorkoutLogEntrySets_WeightUnitId",
                table: "WorkoutLogEntrySets");

            migrationBuilder.DropColumn(
                name: "WeightUnitId",
                table: "WorkoutLogEntrySets");

            migrationBuilder.RenameColumn(
                name: "Weight",
                table: "WorkoutLogEntrySets",
                newName: "WeightLbs");

            migrationBuilder.AddColumn<decimal>(
                name: "WeightKg",
                table: "WorkoutLogEntrySets",
                type: "decimal(18,2)",
                nullable: true);
        }
    }
}
