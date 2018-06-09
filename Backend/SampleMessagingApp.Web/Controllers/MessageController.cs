using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using SampleMessagingApp.Core.Model.Identity;
using SampleMessagingApp.Core.Services.Jwt;
using SampleMessagingApp.Core.Web.DTO.Requests;

namespace SampleMessagingApp.Web.Controllers
{
    [Route("api/message")]
    public class TokenController : Controller
    {
        public TokenController(IJwtService jwtService, UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager)
        {
        }

    }
}
