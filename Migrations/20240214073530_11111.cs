using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MyBlogApi.Migrations
{
    public partial class _11111 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Name",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "Password",
                table: "AspNetUsers");

            migrationBuilder.AlterColumn<string>(
                name: "UserName",
                table: "AspNetUsers",
                type: "nvarchar(256)",
                maxLength: 256,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(256)",
                oldMaxLength: 256,
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ApplicationUserId",
                table: "Article",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Article_ApplicationUserId",
                table: "Article",
                column: "ApplicationUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Article_UserId",
                table: "Article",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Article_AspNetUsers_ApplicationUserId",
                table: "Article",
                column: "ApplicationUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Article_AspNetUsers_UserId",
                table: "Article",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Article_AspNetUsers_ApplicationUserId",
                table: "Article");

            migrationBuilder.DropForeignKey(
                name: "FK_Article_AspNetUsers_UserId",
                table: "Article");

            migrationBuilder.DropIndex(
                name: "IX_Article_ApplicationUserId",
                table: "Article");

            migrationBuilder.DropIndex(
                name: "IX_Article_UserId",
                table: "Article");

            migrationBuilder.DropColumn(
                name: "ApplicationUserId",
                table: "Article");

            migrationBuilder.AlterColumn<string>(
                name: "UserName",
                table: "AspNetUsers",
                type: "nvarchar(256)",
                maxLength: 256,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(256)",
                oldMaxLength: 256);

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "AspNetUsers",
                type: "nvarchar(200)",
                maxLength: 200,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Password",
                table: "AspNetUsers",
                type: "nvarchar(200)",
                maxLength: 200,
                nullable: false,
                defaultValue: "");
        }
    }
}
