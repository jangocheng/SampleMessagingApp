using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using SampleMessagingApp.Core.Model.Identity;
using SampleMessagingApp.Messaging.Fcm.Services;
using SampleMessagingApp.Messaging.Fcm.Web.DTO;

namespace SampleMessagingApp.Messaging.Fcm.Web
{
    [Route("firebase")]
    public class FirebaseMessagingController : Controller
    {
        private readonly IFirebaseMessagingService service;
        private readonly UserManager<ApplicationUser> userManager;

        public FirebaseMessagingController(IFirebaseMessagingService service, UserManager<ApplicationUser> userManager)
        {
            this.service = service;
            this.userManager = userManager;
        }
        
        [Authorize]
        [HttpPost("messaging.token.register")]
        public async Task<IActionResult> RegisterToken([FromBody] UserRegisterTokenRequest request, CancellationToken cancellationToken)
        {
            var user = await userManager.GetUserAsync(User);

            if (user == null)
            {
                return BadRequest();
            }

            await service.RegisterUserAsync(user, request.RegistrationToken, cancellationToken);

            return Ok();
        }

        [Authorize]
        [HttpPost("messaging.token.refresh")]
        public async Task<IActionResult> RefreshToken([FromBody] UserRefreshTokenRequest request, CancellationToken cancellationToken)
        {
            var user = await userManager.GetUserAsync(User);

            if (user == null)
            {
                return BadRequest();
            }

            await service.UpdateUserRegistrationAsync(user, request.RegistrationToken, cancellationToken);

            return Ok();
        }
    }
}
