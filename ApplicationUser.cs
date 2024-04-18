using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace MyBlogApi
{
    public class ApplicationUser : IdentityUser<int>
    {

        [Required]
        public string Introduction { get; set; } = string.Empty;
        [Required]
        [MaxLength(200)]
        public override string UserName { get; set; }

    }
}
