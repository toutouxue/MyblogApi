

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyBlogApi
{
    public class RecommendArticle
    {
        [Key]
        [Required]
        public int Id { get; set; }
        public int articleId {  get; set; }
        public ArticleBase? article { get; set; }
    }
}
