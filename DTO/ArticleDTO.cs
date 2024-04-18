using MyBlogApi;

namespace MyBlogApi.DTO
{
    public class ArticleDTO
    {
        public int Id { get; set; }
        public string ArticleType { get; set; }=string.Empty;
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; }= string.Empty;
        public string Introduce {  get; set; }=string.Empty;
        public ApplicationUserDTOForArticle? ApplicationUser { get; set; }
        public string ImgPath { get; set; } = string.Empty;
    }
}
