using AutoMapper;
using MyBlogApi.DTO;
using MyBlogApi;

namespace MyBlogApi.Profiles
{


    public class MyBlogApiProfile:Profile
    {
        public MyBlogApiProfile()
        {
            CreateMap<ApplicationUser, ApplicationUserDTOForArticle>();
            CreateMap<ArticleBase, ArticleDTO>();
        }
        
    }
}
