using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace StrengthJournal.DataAccess.Migrations
{
    public partial class SeedExercises : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Exercises",
                columns: new[] { "Id", "CreatedByUserId", "Name", "ParentExerciseId" },
                values: new object[] { new Guid("28a04b6a-c8de-447b-85dc-809412967b8e"), null, "Squat", null });

            migrationBuilder.InsertData(
                table: "Exercises",
                columns: new[] { "Id", "CreatedByUserId", "Name", "ParentExerciseId" },
                values: new object[] { new Guid("6e4239d4-cf8d-4a2e-9e19-bca1ec133496"), null, "Bench Press", null });

            migrationBuilder.InsertData(
                table: "Exercises",
                columns: new[] { "Id", "CreatedByUserId", "Name", "ParentExerciseId" },
                values: new object[] { new Guid("ba1930d9-cd93-4035-bf87-c194444362b7"), null, "Deadlift", null });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Exercises",
                keyColumn: "Id",
                keyValue: new Guid("28a04b6a-c8de-447b-85dc-809412967b8e"));

            migrationBuilder.DeleteData(
                table: "Exercises",
                keyColumn: "Id",
                keyValue: new Guid("6e4239d4-cf8d-4a2e-9e19-bca1ec133496"));

            migrationBuilder.DeleteData(
                table: "Exercises",
                keyColumn: "Id",
                keyValue: new Guid("ba1930d9-cd93-4035-bf87-c194444362b7"));
        }
    }
}
