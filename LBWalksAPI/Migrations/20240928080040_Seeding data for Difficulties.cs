using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace LBWalksAPI.Migrations
{
    /// <inheritdoc />
    public partial class SeedingdataforDifficulties : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Difficulties",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { new Guid("67267179-0dae-41cf-9a6b-9a5d3355bcdc"), "Medium" },
                    { new Guid("7b021da9-5874-4c98-ab23-8345a1d112a1"), "Hard" },
                    { new Guid("8fa19201-af46-4777-831e-ed04970f0b6a"), "Easy" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Difficulties",
                keyColumn: "Id",
                keyValue: new Guid("67267179-0dae-41cf-9a6b-9a5d3355bcdc"));

            migrationBuilder.DeleteData(
                table: "Difficulties",
                keyColumn: "Id",
                keyValue: new Guid("7b021da9-5874-4c98-ab23-8345a1d112a1"));

            migrationBuilder.DeleteData(
                table: "Difficulties",
                keyColumn: "Id",
                keyValue: new Guid("8fa19201-af46-4777-831e-ed04970f0b6a"));
        }
    }
}
