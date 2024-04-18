using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using MyBlogApi.DTO;
using MyBlogApi;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace MyBlogApi.Controllers
{
    [Route("api/authentication")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private readonly MyBlogApiContext _myBlogApiContext;
        private readonly IConfiguration _configuration;

        public AuthenticationController(MyBlogApiContext myBlogApiContext,IConfiguration configuration)
        {
            this._myBlogApiContext = myBlogApiContext;
            this._configuration = configuration;
        }
        [HttpPost("Authenticate")]
        public ActionResult<ApplicationUserDTO> Authenticate(AuthenticationRequestBody AuthenticationRequestBody)
        {
            //1.validate the username/password
            var user = ValidateUserCredentials(AuthenticationRequestBody.Email, AuthenticationRequestBody.Password);
            if (user == null) return NotFound();
            //2 create a token
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Authentication:SecretForKey"]));
            var signingCredentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
            // The claims that
            var claimsForToken = new List<Claim>()
            {
                new Claim("sub",user.UserName),
                new Claim("emial",user.Email),
            };
            var jwtSecurityToken = new JwtSecurityToken(
                    _configuration["Authentication:Issuer"],
                    _configuration["Authentication:Audience"],
                    claimsForToken,
                    DateTime.UtcNow,
                    DateTime.UtcNow.AddHours(1),
                    signingCredentials
                );
            var tokenToReturen=new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken);
            return Ok(new ApplicationUserDTO()
            {
                UserId = user.Id,
                Username = user.UserName,
                Email = user.Email,
                Introduction = user.Introduction,
                Token=tokenToReturen,
            });
        }

        private ApplicationUser ValidateUserCredentials(string? email, string? password)
        {
            var u = _myBlogApiContext.Users.FirstOrDefault(u => u.Email == email && u.PasswordHash == password);
            if (u != null)
                return u;
            return null;
        }

        public class AuthenticationRequestBody
        {
            public string? Email { get; set; }
            public string? Password { get; set; }
        }
    }
}
