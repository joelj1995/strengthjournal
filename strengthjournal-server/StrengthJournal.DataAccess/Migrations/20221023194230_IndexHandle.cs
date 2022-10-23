using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace StrengthJournal.DataAccess.Migrations
{
    public partial class IndexHandle : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
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

            migrationBuilder.InsertData(
                table: "Exercises",
                columns: new[] { "Id", "CreatedByUserId", "Name", "ParentExerciseId" },
                values: new object[] { new Guid("84e827a0-ca29-42f8-9b82-c0a79886fc30"), null, "Deadlift", null });

            migrationBuilder.InsertData(
                table: "Exercises",
                columns: new[] { "Id", "CreatedByUserId", "Name", "ParentExerciseId" },
                values: new object[] { new Guid("bd99b901-26eb-4f12-90ca-91d6a7cc673d"), null, "Squat", null });

            migrationBuilder.InsertData(
                table: "Exercises",
                columns: new[] { "Id", "CreatedByUserId", "Name", "ParentExerciseId" },
                values: new object[] { new Guid("f06fdf7f-76c0-4c67-8c3a-994009e75d5a"), null, "Bench Press", null });

            migrationBuilder.CreateIndex(
                name: "IX_Users_Handle",
                table: "Users",
                column: "Handle");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Users_Handle",
                table: "Users");

            migrationBuilder.DeleteData(
                table: "Exercises",
                keyColumn: "Id",
                keyValue: new Guid("84e827a0-ca29-42f8-9b82-c0a79886fc30"));

            migrationBuilder.DeleteData(
                table: "Exercises",
                keyColumn: "Id",
                keyValue: new Guid("bd99b901-26eb-4f12-90ca-91d6a7cc673d"));

            migrationBuilder.DeleteData(
                table: "Exercises",
                keyColumn: "Id",
                keyValue: new Guid("f06fdf7f-76c0-4c67-8c3a-994009e75d5a"));

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
    }
}
