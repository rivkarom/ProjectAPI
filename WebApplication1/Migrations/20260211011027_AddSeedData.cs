using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ChineseAuctionProject.Migrations
{
    /// <inheritdoc />
    public partial class AddSeedData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "Email", "HashPassword", "IsAdmin", "OrdersList", "Phone", "UserName" },
                values: new object[] { "123456789", "y@gmail.com", "יהודית", true, "[]", "0501234567", "Manager" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: "123456789");
        }
    }
}
