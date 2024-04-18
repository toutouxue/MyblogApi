using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MyBlogApi.DTO;
using MyBlogApi.Store;
namespace MyBlogApi.Controllers
{
    [ApiController]
    [Route("api/Home")]
    
    public class HomeController : ControllerBase
    {
        private readonly IArticleStore articleStore;
        private readonly IMapper _mapper;

        public HomeController(IArticleStore articleStore,IMapper mapper)
        {
            this.articleStore = articleStore;
            this._mapper = mapper;
        }
        [HttpGet("GetArticles")]
        public IEnumerable<ArticleBase> GetArticles()
        {
            var articles=articleStore.GetArticles();
            foreach (var item in articles)
            {
                //item.ImgPath = "http://localhost:5209/Img" + item.ImgPath+".png";
            }
            return articles;
        }
        [HttpGet("GetArticleById/{id}")]
        public IActionResult GetArticleById(int id)
        {
            var article=articleStore.GetArticleById(id);
            if (article == null) return NotFound();
            var userDto = _mapper.Map<ApplicationUserDTOForArticle>(article.User);
            
            var articleDto = _mapper.Map<ArticleDTO>(article);
            articleDto.ApplicationUser = userDto;
            return Ok(articleDto);
        }
        [HttpGet("GetMoreArticles")]
        public IEnumerable<ArticleDTO> GetMoreArticles(int lastIndex, int takeSum )
        {
            var articles=articleStore.GetMoreArticles(lastIndex, takeSum);
            var articleDTOs = articles.Select(a =>
            {
                var u=_mapper.Map<ApplicationUserDTOForArticle>(a.User);
                var articleDto=_mapper.Map<ArticleDTO>(a);
                articleDto.ApplicationUser = u;
                return articleDto;
            });
            return articleDTOs;
        }
        
        [HttpPost("AddArticle")]
        [Authorize]
        public int AddArticle(ArticleBase article)
        {
            var imgStore = new ImageStore();
            if (!string.IsNullOrEmpty(article.ImgPath))
            {
                article.ImgPath = imgStore.SaveImageFiles(imgStore.Base64StringToImage(article.ImgPath));
            }
            return articleStore.AddArticle(article);

        }
        [HttpPut("DelectArticle")]
        [Authorize]
        public int DelectArticle(ArticleBase article)=>articleStore.DelectArticle( article );
        
        [HttpPost("RevisionArticle")]
        [Authorize]
        public ActionResult<int> RevisionArticleById(ArticleBase article)=>articleStore.RevisionArticleById( article );
        [HttpGet("GetRecommendArticles")]
        public ActionResult GetRecommendArticles()
        {
            var articles = articleStore.GetRecommendArticles();
            var articleDTOs = articles.Select(a =>
            {
                var u = _mapper.Map<ApplicationUserDTOForArticle>(a.User);
                var articleDto = _mapper.Map<ArticleDTO>(a);
                articleDto.ApplicationUser = u;
                return articleDto;
            });
            if (articleDTOs!=null)
            {
                return Ok(articleDTOs);
            }
            return  BadRequest();
        }
        
    }
}
