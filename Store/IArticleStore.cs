using Microsoft.AspNetCore.Mvc;
using MyBlogApi;

namespace MyBlogApi.Store
{
    public interface IArticleStore
    {
        int AddArticle(Article article);
        int AddArticle(ArticleBase article);
        int DelectArticle(ArticleBase article);
        IEnumerable<ArticleBase> GetArticles();
        IEnumerable<ArticleBase> GetMoreArticles(int lastIndex, int takeSum);
        int RevisionArticleById(ArticleBase article);
        ArticleBase GetArticleById(int id);
        IEnumerable<ArticleBase> GetRecommendArticles();

    }
}