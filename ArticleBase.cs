using MyBlogApi.DTO;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyBlogApi
{
    public class ArticleBase
    {
        [Key]
        [Required]
        public int Id { get; set; }
        public string Introduce {  get; set; }=string.Empty;
        public string Description { get; set; }=string.Empty;
        
        [MaxLength(200)]
        public string Title { get; set; } = string.Empty;
        public string ImgPath { get; set; } = string.Empty;
        //有问题
        public  ArticleType ArticleType { get; set; }
        
        public int UserId {  get; set; }
        public ApplicationUser? User { get; set; }
        
    }
   
}