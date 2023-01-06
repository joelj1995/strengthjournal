using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace StrengthJournal.DataAccess.Migrations
{
    public partial class AddPreferredWeightToUser : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "PreferredWeightUnitId",
                table: "Users",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("BF8DF35B-2F45-4A79-A49C-D3ACA4A12CD6"));

            migrationBuilder.CreateIndex(
                name: "IX_Users_PreferredWeightUnitId",
                table: "Users",
                column: "PreferredWeightUnitId");

            migrationBuilder.AddForeignKey(
                name: "FK_Users_WeightUnits_PreferredWeightUnitId",
                table: "Users",
                column: "PreferredWeightUnitId",
                principalTable: "WeightUnits",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Users_WeightUnits_PreferredWeightUnitId",
                table: "Users");

            migrationBuilder.DropIndex(
                name: "IX_Users_PreferredWeightUnitId",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "PreferredWeightUnitId",
                table: "Users");
        }
    }
}
