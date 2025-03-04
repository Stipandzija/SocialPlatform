using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace ShakSphere.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class initialmigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_FriendRequests_ApplicationUsers_ReceiverId",
                table: "FriendRequests");

            migrationBuilder.DropForeignKey(
                name: "FK_PostComment_ApplicationUsers_AppUserId",
                table: "PostComment");

            migrationBuilder.DropForeignKey(
                name: "FK_PostComment_Posts_PostId",
                table: "PostComment");

            migrationBuilder.DropForeignKey(
                name: "FK_PostInteraction_Posts_PostId",
                table: "PostInteraction");

            migrationBuilder.DropForeignKey(
                name: "FK_UserFriends_ApplicationUsers_UserId",
                table: "UserFriends");

            migrationBuilder.DropPrimaryKey(
                name: "PK_UserFriends",
                table: "UserFriends");

            migrationBuilder.DropIndex(
                name: "IX_UserFriends_UserId",
                table: "UserFriends");

            migrationBuilder.DropPrimaryKey(
                name: "PK_PostInteraction",
                table: "PostInteraction");

            migrationBuilder.DropPrimaryKey(
                name: "PK_PostComment",
                table: "PostComment");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "a13ba62e-f4e5-4276-b11e-c83f5d3b5b3a");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "d6216b03-191d-4aae-bfff-35c273a03ba6");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "UserFriends");

            migrationBuilder.DropColumn(
                name: "CreatedAt",
                table: "FriendRequests");

            migrationBuilder.RenameTable(
                name: "PostInteraction",
                newName: "PostInteractions");

            migrationBuilder.RenameTable(
                name: "PostComment",
                newName: "PostComments");

            migrationBuilder.RenameIndex(
                name: "IX_PostInteraction_PostId",
                table: "PostInteractions",
                newName: "IX_PostInteractions_PostId");

            migrationBuilder.RenameIndex(
                name: "IX_PostComment_PostId",
                table: "PostComments",
                newName: "IX_PostComments_PostId");

            migrationBuilder.RenameIndex(
                name: "IX_PostComment_AppUserId",
                table: "PostComments",
                newName: "IX_PostComments_AppUserId");

            migrationBuilder.AddColumn<DateTime>(
                name: "DateResponded",
                table: "FriendRequests",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DateSent",
                table: "FriendRequests",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_UserFriends",
                table: "UserFriends",
                columns: new[] { "UserId", "FriendId" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_PostInteractions",
                table: "PostInteractions",
                column: "InteractionId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_PostComments",
                table: "PostComments",
                column: "CommentId");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "bafa922a-0c7c-4219-9c12-2c1ea57430d3", null, "User", "USER" },
                    { "d1598288-6f2d-404e-a321-49d0ec6cc05f", null, "Admin", "ADMIN" }
                });

            migrationBuilder.AddForeignKey(
                name: "FK_FriendRequests_ApplicationUsers_ReceiverId",
                table: "FriendRequests",
                column: "ReceiverId",
                principalTable: "ApplicationUsers",
                principalColumn: "AppUserId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_PostComments_ApplicationUsers_AppUserId",
                table: "PostComments",
                column: "AppUserId",
                principalTable: "ApplicationUsers",
                principalColumn: "AppUserId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_PostComments_Posts_PostId",
                table: "PostComments",
                column: "PostId",
                principalTable: "Posts",
                principalColumn: "PostId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PostInteractions_Posts_PostId",
                table: "PostInteractions",
                column: "PostId",
                principalTable: "Posts",
                principalColumn: "PostId",
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
                name: "FK_FriendRequests_ApplicationUsers_ReceiverId",
                table: "FriendRequests");

            migrationBuilder.DropForeignKey(
                name: "FK_PostComments_ApplicationUsers_AppUserId",
                table: "PostComments");

            migrationBuilder.DropForeignKey(
                name: "FK_PostComments_Posts_PostId",
                table: "PostComments");

            migrationBuilder.DropForeignKey(
                name: "FK_PostInteractions_Posts_PostId",
                table: "PostInteractions");

            migrationBuilder.DropForeignKey(
                name: "FK_UserFriends_ApplicationUsers_UserId",
                table: "UserFriends");

            migrationBuilder.DropPrimaryKey(
                name: "PK_UserFriends",
                table: "UserFriends");

            migrationBuilder.DropPrimaryKey(
                name: "PK_PostInteractions",
                table: "PostInteractions");

            migrationBuilder.DropPrimaryKey(
                name: "PK_PostComments",
                table: "PostComments");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "bafa922a-0c7c-4219-9c12-2c1ea57430d3");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "d1598288-6f2d-404e-a321-49d0ec6cc05f");

            migrationBuilder.DropColumn(
                name: "DateResponded",
                table: "FriendRequests");

            migrationBuilder.DropColumn(
                name: "DateSent",
                table: "FriendRequests");

            migrationBuilder.RenameTable(
                name: "PostInteractions",
                newName: "PostInteraction");

            migrationBuilder.RenameTable(
                name: "PostComments",
                newName: "PostComment");

            migrationBuilder.RenameIndex(
                name: "IX_PostInteractions_PostId",
                table: "PostInteraction",
                newName: "IX_PostInteraction_PostId");

            migrationBuilder.RenameIndex(
                name: "IX_PostComments_PostId",
                table: "PostComment",
                newName: "IX_PostComment_PostId");

            migrationBuilder.RenameIndex(
                name: "IX_PostComments_AppUserId",
                table: "PostComment",
                newName: "IX_PostComment_AppUserId");

            migrationBuilder.AddColumn<Guid>(
                name: "Id",
                table: "UserFriends",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedAt",
                table: "FriendRequests",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddPrimaryKey(
                name: "PK_UserFriends",
                table: "UserFriends",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_PostInteraction",
                table: "PostInteraction",
                column: "InteractionId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_PostComment",
                table: "PostComment",
                column: "CommentId");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "a13ba62e-f4e5-4276-b11e-c83f5d3b5b3a", null, "User", "USER" },
                    { "d6216b03-191d-4aae-bfff-35c273a03ba6", null, "Admin", "ADMIN" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_UserFriends_UserId",
                table: "UserFriends",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_FriendRequests_ApplicationUsers_ReceiverId",
                table: "FriendRequests",
                column: "ReceiverId",
                principalTable: "ApplicationUsers",
                principalColumn: "AppUserId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PostComment_ApplicationUsers_AppUserId",
                table: "PostComment",
                column: "AppUserId",
                principalTable: "ApplicationUsers",
                principalColumn: "AppUserId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_PostComment_Posts_PostId",
                table: "PostComment",
                column: "PostId",
                principalTable: "Posts",
                principalColumn: "PostId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PostInteraction_Posts_PostId",
                table: "PostInteraction",
                column: "PostId",
                principalTable: "Posts",
                principalColumn: "PostId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UserFriends_ApplicationUsers_UserId",
                table: "UserFriends",
                column: "UserId",
                principalTable: "ApplicationUsers",
                principalColumn: "AppUserId");
        }
    }
}
