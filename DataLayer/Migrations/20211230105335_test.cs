using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataLayer.Migrations
{
    public partial class test : Migration
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
                    IsTrusted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.UserId);
                });

            migrationBuilder.CreateTable(
                name: "Resources",
                columns: table => new
                {
                    ResourceId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
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
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateTable(
                name: "Comments",
                columns: table => new
                {
                    CommentId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CommentContent = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    NumberOfLikes = table.Column<int>(type: "int", nullable: false),
                    NumberOfDislikes = table.Column<int>(type: "int", nullable: false),
                    CommentOwnerId = table.Column<int>(type: "int", nullable: false),
                    ParentCommentId = table.Column<int>(type: "int", nullable: true),
                    TimeOfPosting = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ResourceId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Comments", x => x.CommentId);
                    table.ForeignKey(
                        name: "FK_Comments_Resources_ResourceId",
                        column: x => x.ResourceId,
                        principalTable: "Resources",
                        principalColumn: "ResourceId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Comments_Users_CommentOwnerId",
                        column: x => x.CommentOwnerId,
                        principalTable: "Users",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "UserId", "IsTrusted", "Password", "RepPoints", "Role", "UserName" },
                values: new object[] { 1, true, "12345", 1, "Admin", "Ivan Bakotin" });

            migrationBuilder.InsertData(
                table: "Resources",
                columns: new[] { "ResourceId", "NameTag", "NumberOfDislikes", "NumberOfLikes", "NumberOfReplys", "ResourceContent", "ResourceOwnerId", "TimeOfPosting" },
                values: new object[] { 1, "Dev", 7, 7, 7, "Fritule su najbolje slatko", 1, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) });

            migrationBuilder.InsertData(
                table: "Comments",
                columns: new[] { "CommentId", "CommentContent", "CommentOwnerId", "NumberOfDislikes", "NumberOfLikes", "ParentCommentId", "ResourceId", "TimeOfPosting" },
                values: new object[] { 1, "Fritule su bezveze", 1, 4, 4, null, 1, new DateTime(2021, 10, 10, 0, 0, 0, 0, DateTimeKind.Unspecified) });

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
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Comments");

            migrationBuilder.DropTable(
                name: "Resources");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
