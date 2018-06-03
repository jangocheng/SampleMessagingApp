using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using SampleMessagingApp.Core.Model.Identity;
using SampleMessagingApp.Core.Services.Jwt;
using SampleMessagingApp.Core.Web.DTO.Requests;
using SampleMessagingApp.Core.Web.Model;

namespace SampleMessagingApp.Web.Controllers
{
    [Route("auth")]
    public class AuthController : Controller
    {
        private readonly IJwtService jwtService;
        private readonly UserManager<ApplicationUser> userManager;
        private readonly SignInManager<ApplicationUser> signInManager;

        public AuthController(IJwtService jwtService, UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager)
        {
            this.jwtService = jwtService;
            this.userManager = userManager;
            this.signInManager = signInManager;
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Login([FromBody] AuthenticationRequest credentials)
        {
            if (credentials == null)
            {
                return BadRequest();
            }

            var user = await userManager.FindByEmailAsync(credentials.Username);

            if (user != null)
            {
                var result = await signInManager.CheckPasswordSignInAsync(user, credentials.Password, lockoutOnFailure: true);

                var claims = await userManager.GetClaimsAsync(user);

                // Additional Token Claims:
                claims.Add(new Claim(JwtRegisteredClaimNames.Sub, user.UserName));

                if (result.Succeeded)
                {
                    return Ok(new { token = jwtService.CreateToken(claims) });
                }
            }

            return Unauthorized();
        }
    }
}
