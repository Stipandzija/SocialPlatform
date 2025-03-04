using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace ShakSphere.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class dodancascadedelete : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_FriendRequests_ApplicationUsers_ReceiverId",
                table: "FriendRequests");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "28966561-7fc9-41f9-8278-2972dfbe00e1");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "4cc5ea6a-6e5d-4c36-b768-151d37fc3d77");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "a13ba62e-f4e5-4276-b11e-c83f5d3b5b3a", null, "User", "USER" },
                    { "d6216b03-191d-4aae-bfff-35c273a03ba6", null, "Admin", "ADMIN" }
                });

            migrationBuilder.AddForeignKey(
                name: "FK_FriendRequests_ApplicationUsers_ReceiverId",
                table: "FriendRequests",
                column: "ReceiverId",
                principalTable: "ApplicationUsers",
                principalColumn: "AppUserId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_FriendRequests_ApplicationUsers_ReceiverId",
                table: "FriendRequests");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "a13ba62e-f4e5-4276-b11e-c83f5d3b5b3a");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "d6216b03-191d-4aae-bfff-35c273a03ba6");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "28966561-7fc9-41f9-8278-2972dfbe00e1", null, "Admin", "ADMIN" },
                    { "4cc5ea6a-6e5d-4c36-b768-151d37fc3d77", null, "User", "USER" }
                });

            migrationBuilder.AddForeignKey(
                name: "FK_FriendRequests_ApplicationUsers_ReceiverId",
                table: "FriendRequests",
                column: "ReceiverId",
                principalTable: "ApplicationUsers",
                principalColumn: "AppUserId",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
