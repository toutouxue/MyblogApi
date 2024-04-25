namespace MyBlogApi.DTO
{
    public class ApplicationUserDTOForArticle
    {
        public string PhotoPath { get; set; } = "https://42.193.13.167:1433/home/ubuntu/Img/dotnet-logo.png";
        public string Introduction { get; set; }=string.Empty;
        public int Id { get; set; }
        public string UserName { get; set; } = string.Empty;
    }
}