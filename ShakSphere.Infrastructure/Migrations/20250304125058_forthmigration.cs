﻿using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace ShakSphere.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class forthmigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_FriendRequests_ApplicationUsers_SenderId",
                table: "FriendRequests");

            migrationBuilder.DropForeignKey(
                name: "FK_UserFriends_ApplicationUsers_FriendId",
                table: "UserFriends");

            migrationBuilder.DropIndex(
                name: "IX_UserFriends_FriendId",
                table: "UserFriends");

            migrationBuilder.DropPrimaryKey(
                name: "PK_FriendRequests",
                table: "FriendRequests");

            migrationBuilder.DropIndex(
                name: "IX_FriendRequests_SenderId",
                table: "FriendRequests");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "45a50614-b848-4f1a-a6f5-5dd778b85990");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "da54ae7e-8cf9-4102-abb1-56e3d45aeeb2");

            migrationBuilder.AddColumn<Guid>(
                name: "UserId",
                table: "UserFriends",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AlterColumn<string>(
                name: "IdentityId",
                table: "ApplicationUsers",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

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

            migrationBuilder.CreateIndex(
                name: "IX_ApplicationUsers_IdentityId",
                table: "ApplicationUsers",
                column: "IdentityId",
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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_FriendRequests_ApplicationUsers_RequestId",
                table: "FriendRequests");

            migrationBuilder.DropForeignKey(
                name: "FK_UserFriends_ApplicationUsers_UserId",
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

            migrationBuilder.DropIndex(
                name: "IX_ApplicationUsers_IdentityId",
                table: "ApplicationUsers");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "55e455aa-d737-4de0-ad19-4ce04e0ae861");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "9457bb20-6bad-4a32-a7db-188e7fecafc1");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "UserFriends");

            migrationBuilder.AlterColumn<string>(
                name: "IdentityId",
                table: "ApplicationUsers",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AddPrimaryKey(
                name: "PK_FriendRequests",
                table: "FriendRequests",
                column: "RequestId");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "45a50614-b848-4f1a-a6f5-5dd778b85990", null, "User", "USER" },
                    { "da54ae7e-8cf9-4102-abb1-56e3d45aeeb2", null, "Admin", "ADMIN" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_UserFriends_FriendId",
                table: "UserFriends",
                column: "FriendId");

            migrationBuilder.CreateIndex(
                name: "IX_FriendRequests_SenderId",
                table: "FriendRequests",
                column: "SenderId");

            migrationBuilder.AddForeignKey(
                name: "FK_FriendRequests_ApplicationUsers_SenderId",
                table: "FriendRequests",
                column: "SenderId",
                principalTable: "ApplicationUsers",
                principalColumn: "AppUserId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UserFriends_ApplicationUsers_FriendId",
                table: "UserFriends",
                column: "FriendId",
                principalTable: "ApplicationUsers",
                principalColumn: "AppUserId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
