namespace MyBlogApi.DTO
{
    public class ApplicationUserDTOForArticle
    {
        public string PhotoPath { get; set; } = "https://localhost:7114/Img/dotnet-logo.png";
        public string Introduction { get; set; }=string.Empty;
        public int Id { get; set; }
        public string UserName { get; set; } = string.Empty;
    }
}