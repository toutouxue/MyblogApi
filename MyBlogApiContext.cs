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
        public MyBlogApiContext(DbContextOptions<MyBlogApiContext> options):base(options)
        {
            
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            
        }
    }
}
