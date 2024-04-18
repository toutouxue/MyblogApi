using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MyBlogApi.Migrations
{
    public partial class RemoveApplicationUserIdFKInArticle : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Article_ApplicationUserId",
                table: "Article"
                );
            migrationBuilder.DropForeignKey(
                name: "FK_Article_AspNetUsers_ApplicationUserId",
                table: "Article");
            migrationBuilder.DropColumn(
                name:"ApplicationUserId",
                table: "Article"
                );
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_Article_ApplicationUserId",
                table: "Article",
                column: "ApplicationUserId");
            migrationBuilder.AddForeignKey(
                name: "FK_Article_AspNetUsers_ApplicationUserId",
                table: "Article",
                column: "ApplicationUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id"
                );
            migrationBuilder.AddColumn<int>(
                name: "ApplicationUserId",
                table: "Article",
                type: "int",
                nullable: true
                );
        }
    }
}
