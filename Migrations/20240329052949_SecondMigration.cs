using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace IT_Service_Deck.Migrations
{
    /// <inheritdoc />
    public partial class SecondMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "41fdd333-3c08-4d87-8c53-0d0aae75eb56", null, "admin", "admin" },
                    { "7de4c757-4c7b-4dda-826c-dc278f14ce5b", null, "approver", "approver" },
                    { "a2d9e16d-55b0-4af0-8200-890582329946", null, "employee", "employee" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "41fdd333-3c08-4d87-8c53-0d0aae75eb56");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "7de4c757-4c7b-4dda-826c-dc278f14ce5b");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "a2d9e16d-55b0-4af0-8200-890582329946");
        }
    }
}
