using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyBlogApi;

namespace MyBlogApi.Store
{
    public class ArticleStore : IArticleStore
    {
        private readonly MyBlogApiContext dbContext;

        public ArticleStore(MyBlogApiContext dbContext)
        {
            this.dbContext = dbContext;
        }
        public int AddArticle(Article article)
        {
            dbContext.Article.Add(article);
            return dbContext.SaveChanges();
        }
        public IEnumerable<ArticleBase> GetArticles()
        {
            return dbContext.Article.ToList();
        }
        public IEnumerable<ArticleBase> GetMoreArticles(int lastIndex, int takeSum)
        {
            var articles = dbContext.Article.Include(a=>a.User).AsNoTracking().Skip(lastIndex).Take(takeSum).ToList();
            foreach (var item in articles)
            {
                item.ImgPath = "https://42.193.13.167:1433/home/ubuntu/Img/" + item.ImgPath + ".png";
            }
            return articles;
        }
        public int AddArticle(ArticleBase article)
        {
            dbContext.Article.Add(article);
            return dbContext.SaveChanges();
        }
        public int DelectArticle(ArticleBase article)
        {
            dbContext.Article.Remove(article);

            return dbContext.SaveChanges();
        }
        public int RevisionArticleById(ArticleBase article)
        {
            var a = dbContext.Article.FirstOrDefault(a => a.Id == article.Id);
            a.Title = article.Title;
            a.Description = article.Description;
            a.UserId = article.UserId;
            dbContext.Update(a);
            return dbContext.SaveChanges();
        }
        public ArticleBase GetArticleById(int id)
        {
            return dbContext.Article.Include(u=>u.User).FirstOrDefault(a => a.Id == id);
        }

        public IEnumerable<ArticleBase> GetRecommendArticles()
        {
            var a= from articles
                   in dbContext.Article.Include(a => a.User)
                    join recommendArticle in dbContext.recommendArticles
                    on articles.Id equals recommendArticle.articleId
                    select articles;
            foreach (var item in a)
            {
                item.ImgPath = "https://42.193.13.167:1433/home/ubuntu/Img/" + item.ImgPath + ".png";
            }
            return a;
        }
    }
}
