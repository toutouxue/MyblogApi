using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MyBlogApi.Migrations
{
    public partial class AddRecommedArticleTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {

            migrationBuilder.CreateTable(
                name: "recommendArticles",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    articleId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_recommendArticles", x => x.Id);
                    table.ForeignKey(
                        name: "FK_recommendArticles_Article_articleId",
                        column: x => x.articleId,
                        principalTable: "Article",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_recommendArticles_articleId",
                table: "recommendArticles",
                column: "articleId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "recommendArticles");

         
        }
    }
}
