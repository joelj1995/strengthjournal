using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace StrengthJournal.DataAccess.Migrations
{
    public partial class AddKgRatioColumnToWeightUnit : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "RatioToKg",
                table: "WeightUnits",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.UpdateData(
                table: "WeightUnits",
                keyColumn: "Id",
                keyValue: new Guid("4bc96550-f274-4a90-978b-92a398f8c49d"),
                column: "RatioToKg",
                value: 1.0m);

            migrationBuilder.UpdateData(
                table: "WeightUnits",
                keyColumn: "Id",
                keyValue: new Guid("bf8df35b-2f45-4a79-a49c-d3aca4a12cd6"),
                column: "RatioToKg",
                value: 2.2m);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "RatioToKg",
                table: "WeightUnits");
        }
    }
}
