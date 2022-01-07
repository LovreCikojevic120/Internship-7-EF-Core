using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataLayer.Migrations
{
    public partial class finalMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    UserId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Role = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    RepPoints = table.Column<int>(type: "int", nullable: false),
                    IsTrusted = table.Column<bool>(type: "bit", nullable: false),
                    IsDeactivated = table.Column<bool>(type: "bit", nullable: false),
                    DeactivatedUntil = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.UserId);
                });

            migrationBuilder.CreateTable(
                name: "Resources",
                columns: table => new
                {
                    ResourceId = table.Column<int>(type: "int", nullable: false),
                    ResourceContent = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ResourceOwnerId = table.Column<int>(type: "int", nullable: false),
                    NumberOfReplys = table.Column<int>(type: "int", nullable: false),
                    NumberOfLikes = table.Column<int>(type: "int", nullable: false),
                    NumberOfDislikes = table.Column<int>(type: "int", nullable: false),
                    TimeOfPosting = table.Column<DateTime>(type: "datetime2", nullable: false),
                    NameTag = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Resources", x => x.ResourceId);
                    table.ForeignKey(
                        name: "FK_Resources_Users_ResourceOwnerId",
                        column: x => x.ResourceOwnerId,
                        principalTable: "Users",
                        principalColumn: "UserId");
                });

            migrationBuilder.CreateTable(
                name: "Comments",
                columns: table => new
                {
                    CommentId = table.Column<int>(type: "int", nullable: false),
                    CommentContent = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    NumberOfLikes = table.Column<int>(type: "int", nullable: false),
                    NumberOfDislikes = table.Column<int>(type: "int", nullable: false),
                    CommentOwnerId = table.Column<int>(type: "int", nullable: false),
                    ParentCommentId = table.Column<int>(type: "int", nullable: true),
                    TimeOfPosting = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ResourceId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Comments", x => x.CommentId);
                    table.ForeignKey(
                        name: "FK_Comments_Resources_ResourceId",
                        column: x => x.ResourceId,
                        principalTable: "Resources",
                        principalColumn: "ResourceId");
                    table.ForeignKey(
                        name: "FK_Comments_Users_CommentOwnerId",
                        column: x => x.CommentOwnerId,
                        principalTable: "Users",
                        principalColumn: "UserId");
                });

            migrationBuilder.CreateTable(
                name: "UserResources",
                columns: table => new
                {
                    UserId = table.Column<int>(type: "int", nullable: false),
                    ResourceId = table.Column<int>(type: "int", nullable: false),
                    IsVoted = table.Column<bool>(type: "bit", nullable: false),
                    IsCommented = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserResources", x => new { x.UserId, x.ResourceId });
                    table.ForeignKey(
                        name: "FK_UserResources_Resources_ResourceId",
                        column: x => x.ResourceId,
                        principalTable: "Resources",
                        principalColumn: "ResourceId");
                    table.ForeignKey(
                        name: "FK_UserResources_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "UserId");
                });

            migrationBuilder.CreateTable(
                name: "UserComments",
                columns: table => new
                {
                    UserId = table.Column<int>(type: "int", nullable: false),
                    CommentId = table.Column<int>(type: "int", nullable: false),
                    IsVoted = table.Column<bool>(type: "bit", nullable: false),
                    IsCommented = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserComments", x => new { x.UserId, x.CommentId });
                    table.ForeignKey(
                        name: "FK_UserComments_Comments_CommentId",
                        column: x => x.CommentId,
                        principalTable: "Comments",
                        principalColumn: "CommentId");
                    table.ForeignKey(
                        name: "FK_UserComments_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "UserId");
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "UserId", "DeactivatedUntil", "IsDeactivated", "IsTrusted", "Password", "RepPoints", "Role", "UserName" },
                values: new object[] { 1, null, false, true, "12345", 10000, "Organizator", "Jure Juric" });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "UserId", "DeactivatedUntil", "IsDeactivated", "IsTrusted", "Password", "RepPoints", "Role", "UserName" },
                values: new object[] { 2, null, false, false, "Lozinka", 1, "Intern", "Ivan Ivic" });

            migrationBuilder.CreateIndex(
                name: "IX_Comments_CommentOwnerId",
                table: "Comments",
                column: "CommentOwnerId");

            migrationBuilder.CreateIndex(
                name: "IX_Comments_ResourceId",
                table: "Comments",
                column: "ResourceId");

            migrationBuilder.CreateIndex(
                name: "IX_Resources_ResourceOwnerId",
                table: "Resources",
                column: "ResourceOwnerId");

            migrationBuilder.CreateIndex(
                name: "IX_UserComments_CommentId",
                table: "UserComments",
                column: "CommentId");

            migrationBuilder.CreateIndex(
                name: "IX_UserResources_ResourceId",
                table: "UserResources",
                column: "ResourceId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "UserComments");

            migrationBuilder.DropTable(
                name: "UserResources");

            migrationBuilder.DropTable(
                name: "Comments");

            migrationBuilder.DropTable(
                name: "Resources");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
