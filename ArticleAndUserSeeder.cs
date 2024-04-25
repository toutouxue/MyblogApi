using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using MyBlogApi.Store;

namespace MyBlogApi
{
    public class ArticleAndUserSeeder
    {
        private readonly IServiceProvider serviceProvider;
        private readonly MyBlogApiContext myBlogApiContext;
        private readonly ApplicationUserStore userStore;

        public  ArticleAndUserSeeder(IServiceProvider serviceProvider,MyBlogApiContext myBlogApiContext, ApplicationUserStore userStore)
        {
            ArgumentNullException.ThrowIfNull(serviceProvider, nameof(serviceProvider));
            ArgumentNullException.ThrowIfNull(myBlogApiContext, nameof(myBlogApiContext));
            this.serviceProvider = serviceProvider;

            this.myBlogApiContext = myBlogApiContext;
            this.userStore = userStore;
        }
        public async Task SeedTheArticle()
        {
            if (!await myBlogApiContext.Article.AnyAsync())
            {
               await myBlogApiContext.Article.AddRangeAsync(
                    new ArticleBase() { Title="test",UserId = myBlogApiContext.Users.FirstOrDefault().Id },
                    new ArticleBase() { Title = "test1",  UserId= myBlogApiContext.Users.FirstOrDefault().Id },
                    new ArticleBase() { Title = "test2",  UserId = myBlogApiContext.Users.FirstOrDefault().Id }
                    );;
                myBlogApiContext.SaveChanges();
                Console.WriteLine("初始化文章数据成功");
            }
            else
            {
                Console.WriteLine("已经有文章数据了");
            }
        }
        public async Task SeedTheUser()
        {
            if (myBlogApiContext.ApplicationUser.Count()<10)
            {
                
                for (int i = 0; i < 5; i++)
                {
                  var result= await userStore.CreateAsync(new ApplicationUser()
                    {
                        UserName = "TestUser"+i,
                        Introduction = "TestUser"+i,
                        Email = "1234@qq.com",
                        PasswordHash = "12345",
                    }, CancellationToken.None);
                    if (result==IdentityResult.Success)
                    {
                        Console.WriteLine("初始创建用户数据成功");
                    }
                }
                
            }else Console.WriteLine("已经有用户数据了");
        }
        public async void Seed()
        {
            if (!myBlogApiContext.Database.EnsureCreated())
            {
                await SeedTheUser();
                await SeedTheArticle();
            }
            else { 
            
            };
        }
        public void SeedTestData()
        {
            
            Random random = new Random();
            
            int t = myBlogApiContext.Article.Count()+1;
            List<ArticleBase> list = new List<ArticleBase>();
            for (int i = 0; i < 100; i++)
            {
                int r = random.Next(4);
                ArticleBase article = new ArticleBase() { Title = $"test{t}", UserId = r, ArticleType = ArticleType.English, ImgPath = "https://cdn.neow.in/news/images/uploaded/2020/11/1605027417_microsoft_net.jpg" };
                list.Add(article);
                t++;
            }
            myBlogApiContext.Article.AddRange(list);
            myBlogApiContext.SaveChanges();
        }
    }
}
