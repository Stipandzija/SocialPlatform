using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace ShakSphere.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class new1hmigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserFriends_ApplicationUsers_ApplicationUserAppUserId",
                table: "UserFriends");

            migrationBuilder.DropPrimaryKey(
                name: "PK_UserFriends",
                table: "UserFriends");

            migrationBuilder.DropIndex(
                name: "IX_UserFriends_ApplicationUserAppUserId",
                table: "UserFriends");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "5129a34f-cced-488a-9954-a2926dd1ec12");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "bff4701c-2904-40f2-a362-360ac1d968a1");

            migrationBuilder.DropColumn(
                name: "ApplicationUserAppUserId",
                table: "UserFriends");

            migrationBuilder.AddPrimaryKey(
                name: "PK_UserFriends",
                table: "UserFriends",
                column: "Id");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "28966561-7fc9-41f9-8278-2972dfbe00e1", null, "Admin", "ADMIN" },
                    { "4cc5ea6a-6e5d-4c36-b768-151d37fc3d77", null, "User", "USER" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_UserFriends_UserId",
                table: "UserFriends",
                column: "UserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_UserFriends",
                table: "UserFriends");

            migrationBuilder.DropIndex(
                name: "IX_UserFriends_UserId",
                table: "UserFriends");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "28966561-7fc9-41f9-8278-2972dfbe00e1");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "4cc5ea6a-6e5d-4c36-b768-151d37fc3d77");

            migrationBuilder.AddColumn<Guid>(
                name: "ApplicationUserAppUserId",
                table: "UserFriends",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_UserFriends",
                table: "UserFriends",
                columns: new[] { "UserId", "FriendId" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "5129a34f-cced-488a-9954-a2926dd1ec12", null, "Admin", "ADMIN" },
                    { "bff4701c-2904-40f2-a362-360ac1d968a1", null, "User", "USER" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_UserFriends_ApplicationUserAppUserId",
                table: "UserFriends",
                column: "ApplicationUserAppUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_UserFriends_ApplicationUsers_ApplicationUserAppUserId",
                table: "UserFriends",
                column: "ApplicationUserAppUserId",
                principalTable: "ApplicationUsers",
                principalColumn: "AppUserId");
        }
    }
}
