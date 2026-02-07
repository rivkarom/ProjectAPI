using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ChineseAuctionProject.Migrations
{
    /// <inheritdoc />
    public partial class AddDefaultSeed : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "Id", "CreatedAt", "Description", "Icon", "Name", "UpdatedAt" },
                values: new object[] { 2, new DateTime(2026, 2, 11, 4, 37, 10, 48, DateTimeKind.Utc).AddTicks(7492), "קטגוריה דיפולטיבית", "default.png", "קטגוריה ברירת מחדל", new DateTime(2026, 2, 11, 4, 37, 10, 48, DateTimeKind.Utc).AddTicks(7494) });

            migrationBuilder.InsertData(
                table: "Donors",
                columns: new[] { "Id", "Email", "Name", "Phone" },
                values: new object[] { "329084172", "donor@example.com", "תורם ברירת מחדל", "0501234567" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Donors",
                keyColumn: "Id",
                keyValue: "329084172");
        }
    }
}
