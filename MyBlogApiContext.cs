using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace MyBlogApi
{
    public class MyBlogApiContext:IdentityDbContext<ApplicationUser, IdentityRole<int>,int>
    {
        public DbSet<ApplicationUser> ApplicationUser { get; set; }
        public DbSet<ArticleBase> Article { get; set; }
        public DbSet<RecommendArticle> recommendArticles { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                string MssqlConnectionString = "Data Source=DESKTOP-M7V00GP;Database=MyBolgApi;User ID=sa;Password=qq1234!@#;TrustServerCertificate=true";
                optionsBuilder.UseSqlServer(MssqlConnectionString);
            }
        }
    }
}
