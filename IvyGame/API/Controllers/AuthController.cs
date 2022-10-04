using IvyGame.Models.DTO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using IvyGame.Models.Domain;
using System.Security.Claims;

namespace IvyGame.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    public class AuthController : ControllerBase
    {

        public AuthController(UserManager<ApplicationUser> userManager,
            IConfiguration configuration)
        {
            UserManager = userManager;
            Configuration = configuration;
        }
        private UserManager<ApplicationUser> UserManager { get; }
        private IConfiguration Configuration { get; }



        [HttpPost]
        public async Task<ActionResult> Login(CredentialsDto credentialsDto)
        {
            var user = await UserManager.FindByIdAsync(credentialsDto.UserName);
            var hasAccess = await UserManager.CheckPasswordAsync(user, credentialsDto.Password);

            if (hasAccess)
            {
                return Unauthorized();

            }
            var tokenDto = GenerateToken(user);
            return Created("", tokenDto);
        }
        private TokenDto GenerateToken(ApplicationUser applicationUser)
        {
            var signingKey = Convert.FromBase64String(Configuration["Jwt:SigningSecret"]);
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, applicationUser.Id),
                new Claim(ClaimTypes.GivenName, applicationUser.FirstName),
                new Claim(ClaimTypes.Surname, applicationUser.LastName)
            };

            var roles = UserManager.GetRolesAsync(applicationUser).Result;

            foreach (var role in roles)
            {
                claims.Add(new Claim(ClaimTypes.Role, role));
            }
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                SigningCredentials = new SigningCredentials(
               new SymmetricSecurityKey(signingKey),
               SecurityAlgorithms.HmacSha256Signature),

                Expires = DateTime.UtcNow.AddHours(2),

                Subject = new ClaimsIdentity(claims)
            };
            var jwtTokenHandler = new JwtSecurityTokenHandler();

            var jwtSecurityToken = jwtTokenHandler.CreateJwtSecurityToken(tokenDescriptor);
            //sätt Claims Token
            return new TokenDto()
            { Token = jwtTokenHandler.WriteToken(jwtSecurityToken) };
        }


    }
}
