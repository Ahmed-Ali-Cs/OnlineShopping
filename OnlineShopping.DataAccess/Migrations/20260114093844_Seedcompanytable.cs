using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace OnlineShopping.Migrations
{
    /// <inheritdoc />
    public partial class Seedcompanytable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Companies",
                columns: new[] { "Id", "Address", "City", "Name", "PhoneNumber", "PostalCode", "Region" },
                values: new object[,]
                {
                    { 2, "123 Tech Street", "Techville", "TechCorp", -8223, "90001", null },
                    { 3, "456 Book Avenue", "Readertown", "BookWorld", -9234, "90002", null }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Companies",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Companies",
                keyColumn: "Id",
                keyValue: 3);
        }
    }
}
