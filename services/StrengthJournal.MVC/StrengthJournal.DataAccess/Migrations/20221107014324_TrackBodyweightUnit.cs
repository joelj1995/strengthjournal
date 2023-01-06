using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace StrengthJournal.DataAccess.Migrations
{
    public partial class TrackBodyweightUnit : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "BodyWeightPITUnitId",
                table: "WorkoutLogEntries",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_WorkoutLogEntries_BodyWeightPITUnitId",
                table: "WorkoutLogEntries",
                column: "BodyWeightPITUnitId");

            migrationBuilder.AddForeignKey(
                name: "FK_WorkoutLogEntries_WeightUnits_BodyWeightPITUnitId",
                table: "WorkoutLogEntries",
                column: "BodyWeightPITUnitId",
                principalTable: "WeightUnits",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_WorkoutLogEntries_WeightUnits_BodyWeightPITUnitId",
                table: "WorkoutLogEntries");

            migrationBuilder.DropIndex(
                name: "IX_WorkoutLogEntries_BodyWeightPITUnitId",
                table: "WorkoutLogEntries");

            migrationBuilder.DropColumn(
                name: "BodyWeightPITUnitId",
                table: "WorkoutLogEntries");
        }
    }
}
