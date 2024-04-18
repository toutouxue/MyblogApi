using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MyBlogApi.Migrations
{
    public partial class AddPropertyIntroduceInArticleBase : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Introduce",
                table: "Article",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Introduce",
                table: "Article");
        }
    }
}
