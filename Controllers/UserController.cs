using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using MyBlogApi.DTO;
using MyBlogApi.Store;
using MyBlogApi;

namespace MyBlogApi.Controllers
{
    [Route("api/User")]
    [ApiController]
    [Authorize]
    public class UserController : ControllerBase
    {
        private readonly ApplicationUserStore userStore;
        public UserController(ApplicationUserStore userStore)
        {
            this.userStore = userStore;
        }


        // GET: UserController/Details/5
        [HttpGet("GetUserByEmail")]
        public  async Task<ActionResult<ApplicationUser>> GetUserByEmail(ApplicationUserDTO user)
        {
            var u = await userStore.FindByEmailAsync(user.Email, CancellationToken.None);
            await Console.Out.WriteLineAsync(u.Email);
            if (u != null)
            {
                return u;
            }
            return Ok("没找到该用户");
        }
        // POST: UserController/Create
        [HttpPost("Create")]
        
        public async Task<ActionResult<int>> Create(ApplicationUser user)
        {
          var result = await userStore.CreateAsync(user, CancellationToken.None);
          return Ok(result.Succeeded);
        }
        // POST: UserController/Edit/5
        [HttpPost("Edit")]
        public ActionResult<int> Edit( ApplicationUser user)
        {
            try
            {
                var u = userStore.FindByNameAsync(user.UserName, CancellationToken.None);
                if (u!=null)
                {

                }

                return 1;
            }
            catch
            {
                return Ok("修改失败，没有找到该用户");
            }
        }

        // GET: UserController/Delete/5
        [HttpGet("Delete")]
        public ActionResult<int> Delete(int id)
        {
           
                
            return Ok("未找到该用户，删除失败");
        }
    }
}
