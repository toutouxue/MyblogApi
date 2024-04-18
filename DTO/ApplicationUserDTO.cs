namespace MyBlogApi.DTO
{
    public class ApplicationUserDTO 
    {
        public int UserId { get; set; }
        public string Email { get; set; } = string.Empty;
        public string Introduction { get; set; } = string.Empty;
        public string Username { get; set; } = string.Empty;
        public string Token { get; set; } = string.Empty;
    }
}
