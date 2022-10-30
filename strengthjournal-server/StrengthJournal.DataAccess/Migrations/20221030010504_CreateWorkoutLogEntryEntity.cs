using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace StrengthJournal.DataAccess.Migrations
{
    public partial class CreateWorkoutLogEntryEntity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "WorkoutLogEntries",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    EntryDateUTC = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WorkoutLogEntries", x => x.Id);
                    table.ForeignKey(
                        name: "FK_WorkoutLogEntries_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "WorkoutLogEntrySets",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    WorkoutLogEntryId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ExerciseId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Reps = table.Column<long>(type: "bigint", nullable: true),
                    TargetReps = table.Column<long>(type: "bigint", nullable: true),
                    WeightKg = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    WeightLbs = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    RPE = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WorkoutLogEntrySets", x => x.Id);
                    table.ForeignKey(
                        name: "FK_WorkoutLogEntrySets_Exercises_ExerciseId",
                        column: x => x.ExerciseId,
                        principalTable: "Exercises",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_WorkoutLogEntrySets_WorkoutLogEntries_WorkoutLogEntryId",
                        column: x => x.WorkoutLogEntryId,
                        principalTable: "WorkoutLogEntries",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_WorkoutLogEntries_UserId",
                table: "WorkoutLogEntries",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_WorkoutLogEntrySets_ExerciseId",
                table: "WorkoutLogEntrySets",
                column: "ExerciseId");

            migrationBuilder.CreateIndex(
                name: "IX_WorkoutLogEntrySets_WorkoutLogEntryId",
                table: "WorkoutLogEntrySets",
                column: "WorkoutLogEntryId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "WorkoutLogEntrySets");

            migrationBuilder.DropTable(
                name: "WorkoutLogEntries");
        }
    }
}
