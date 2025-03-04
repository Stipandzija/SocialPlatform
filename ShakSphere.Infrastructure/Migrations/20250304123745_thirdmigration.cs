using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace ShakSphere.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class thirdmigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "ac8421e6-6a79-4837-9b23-2856ea8606b4");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "d1b236c5-99cc-4e76-baea-a6a05710e077");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "45a50614-b848-4f1a-a6f5-5dd778b85990", null, "User", "USER" },
                    { "da54ae7e-8cf9-4102-abb1-56e3d45aeeb2", null, "Admin", "ADMIN" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "45a50614-b848-4f1a-a6f5-5dd778b85990");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "da54ae7e-8cf9-4102-abb1-56e3d45aeeb2");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "ac8421e6-6a79-4837-9b23-2856ea8606b4", null, "Admin", "ADMIN" },
                    { "d1b236c5-99cc-4e76-baea-a6a05710e077", null, "User", "USER" }
                });
        }
    }
}
