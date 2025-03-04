using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace ShakSphere.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class newhmigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_FriendRequests_ApplicationUsers_RequestId",
                table: "FriendRequests");

            migrationBuilder.DropForeignKey(
                name: "FK_UserFriends_ApplicationUsers_UserId",
                table: "UserFriends");

            migrationBuilder.DropPrimaryKey(
                name: "PK_UserFriends",
                table: "UserFriends");

            migrationBuilder.DropIndex(
                name: "IX_UserFriends_UserId_FriendId",
                table: "UserFriends");

            migrationBuilder.DropPrimaryKey(
                name: "PK_FriendRequests",
                table: "FriendRequests");

            migrationBuilder.DropIndex(
                name: "IX_FriendRequests_RequestId_ReceiverId",
                table: "FriendRequests");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "55e455aa-d737-4de0-ad19-4ce04e0ae861");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "9457bb20-6bad-4a32-a7db-188e7fecafc1");

            migrationBuilder.AddColumn<Guid>(
                name: "ApplicationUserAppUserId",
                table: "UserFriends",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_UserFriends",
                table: "UserFriends",
                columns: new[] { "UserId", "FriendId" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_FriendRequests",
                table: "FriendRequests",
                column: "RequestId");

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

            migrationBuilder.CreateIndex(
                name: "IX_UserFriends_FriendId",
                table: "UserFriends",
                column: "FriendId");

            migrationBuilder.CreateIndex(
                name: "IX_FriendRequests_ReceiverId",
                table: "FriendRequests",
                column: "ReceiverId");

            migrationBuilder.CreateIndex(
                name: "IX_FriendRequests_SenderId",
                table: "FriendRequests",
                column: "SenderId");

            migrationBuilder.AddForeignKey(
                name: "FK_FriendRequests_ApplicationUsers_ReceiverId",
                table: "FriendRequests",
                column: "ReceiverId",
                principalTable: "ApplicationUsers",
                principalColumn: "AppUserId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_FriendRequests_ApplicationUsers_SenderId",
                table: "FriendRequests",
                column: "SenderId",
                principalTable: "ApplicationUsers",
                principalColumn: "AppUserId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_UserFriends_ApplicationUsers_ApplicationUserAppUserId",
                table: "UserFriends",
                column: "ApplicationUserAppUserId",
                principalTable: "ApplicationUsers",
                principalColumn: "AppUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_UserFriends_ApplicationUsers_FriendId",
                table: "UserFriends",
                column: "FriendId",
                principalTable: "ApplicationUsers",
                principalColumn: "AppUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_UserFriends_ApplicationUsers_UserId",
                table: "UserFriends",
                column: "UserId",
                principalTable: "ApplicationUsers",
                principalColumn: "AppUserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_FriendRequests_ApplicationUsers_ReceiverId",
                table: "FriendRequests");

            migrationBuilder.DropForeignKey(
                name: "FK_FriendRequests_ApplicationUsers_SenderId",
                table: "FriendRequests");

            migrationBuilder.DropForeignKey(
                name: "FK_UserFriends_ApplicationUsers_ApplicationUserAppUserId",
                table: "UserFriends");

            migrationBuilder.DropForeignKey(
                name: "FK_UserFriends_ApplicationUsers_FriendId",
                table: "UserFriends");

            migrationBuilder.DropForeignKey(
                name: "FK_UserFriends_ApplicationUsers_UserId",
                table: "UserFriends");

            migrationBuilder.DropPrimaryKey(
                name: "PK_UserFriends",
                table: "UserFriends");

            migrationBuilder.DropIndex(
                name: "IX_UserFriends_ApplicationUserAppUserId",
                table: "UserFriends");

            migrationBuilder.DropIndex(
                name: "IX_UserFriends_FriendId",
                table: "UserFriends");

            migrationBuilder.DropPrimaryKey(
                name: "PK_FriendRequests",
                table: "FriendRequests");

            migrationBuilder.DropIndex(
                name: "IX_FriendRequests_ReceiverId",
                table: "FriendRequests");

            migrationBuilder.DropIndex(
                name: "IX_FriendRequests_SenderId",
                table: "FriendRequests");

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

            migrationBuilder.AddPrimaryKey(
                name: "PK_FriendRequests",
                table: "FriendRequests",
                column: "SenderId");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "55e455aa-d737-4de0-ad19-4ce04e0ae861", null, "Admin", "ADMIN" },
                    { "9457bb20-6bad-4a32-a7db-188e7fecafc1", null, "User", "USER" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_UserFriends_UserId_FriendId",
                table: "UserFriends",
                columns: new[] { "UserId", "FriendId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_FriendRequests_RequestId_ReceiverId",
                table: "FriendRequests",
                columns: new[] { "RequestId", "ReceiverId" },
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_FriendRequests_ApplicationUsers_RequestId",
                table: "FriendRequests",
                column: "RequestId",
                principalTable: "ApplicationUsers",
                principalColumn: "AppUserId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UserFriends_ApplicationUsers_UserId",
                table: "UserFriends",
                column: "UserId",
                principalTable: "ApplicationUsers",
                principalColumn: "AppUserId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
