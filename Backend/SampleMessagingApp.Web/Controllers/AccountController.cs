// Copyright (c) Philipp Wagner. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using SampleMessagingApp.Core.Model.Identity;
using SampleMessagingApp.Core.Services.Email;
using SampleMessagingApp.Core.Web.DTO.Requests;

namespace SampleMessagingApp.Web.Controllers
{
    public static class UrlHelperExtensions
    {
        public static string EmailConfirmationLink(this IUrlHelper urlHelper, string userId, string code, string scheme)
        {
            return urlHelper.Action(
                action: nameof(AccountController.ConfirmEmail),
                controller: "Account",
                values: new { userId, code },
                protocol: scheme);
        }

        public static string ResetPasswordCallbackLink(this IUrlHelper urlHelper, string userId, string code, string scheme)
        {
            return urlHelper.Action(
                action: nameof(AccountController.ResetPassword),
                controller: "Account",
                values: new { userId, code },
                protocol: scheme);
        }
    }

    [Route("account")]
    public class AccountController : Controller
    {
        private readonly IEmailConfirmationService emailConfirmationService;
        private readonly UserManager<ApplicationUser> userManager;

        public AccountController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, IEmailConfirmationService emailConfirmationService)
        {

            this.userManager = userManager;
            this.emailConfirmationService = emailConfirmationService;
        }

        [HttpPost("register.request")]
        [AllowAnonymous]
        public async Task<IActionResult> Register([FromBody] RegisterAccountRequest request)
        {
            if (request == null)
            {
                return BadRequest();
            }

            var user = new ApplicationUser
            {
                UserName = request.EMail,
                Email = request.EMail
            };

            var result = await userManager.CreateAsync(user, request.Password);

            if (result.Succeeded)
            {
                // Generate Confirmation Token for the User:
                var confirmationCode = await userManager.GenerateEmailConfirmationTokenAsync(user);
                // Generate the Callback URL for the Confirmation:
                var confirmationUrl = Url.EmailConfirmationLink(user.Id, confirmationCode, Request.Scheme);
                // Send the Mail:
                await emailConfirmationService.SendEmailConfirmationAsync(user, confirmationUrl);

                return Ok();
            }

            return BadRequest();
        }

        [HttpPost("register.confirm")]
        [AllowAnonymous]
        public async Task<IActionResult> ConfirmEmail([FromQuery] ConfirmEmailRequest request)
        {
            var user = await userManager.FindByIdAsync(request.UserId);

            if (user == null)
            {
                return BadRequest();
            }

            var result = await userManager.ConfirmEmailAsync(user, request.Code);

            if (result.Succeeded)
            {
                return Ok();
            }

            return BadRequest();
        }

        [HttpGet("reset.request")]
        [AllowAnonymous]
        public async Task<IActionResult> RequestPasswordReset([FromBody] ResetPasswordRequest request)
        {
            var user = await userManager.FindByIdAsync(request.UserId);

            if (user == null)
            {
                return BadRequest();
            }

            // Generate Confirmation Token for the User:
            var resetCode = await userManager.GeneratePasswordResetTokenAsync(user);

            // Generate the Callback URL for the Confirmation:
            var confirmationUrl = Url.ResetPasswordCallbackLink(user.Id, resetCode, Request.Scheme);

            // Send the Mail:
            await emailConfirmationService.SendEmailConfirmationAsync(user, confirmationUrl);

            return Ok();
        }

        [HttpPost("reset.confirm")]
        [AllowAnonymous]
        public async Task<IActionResult> ResetPassword([FromBody] ResetPasswordRequest request)
        {
            var user = await userManager.FindByIdAsync(request.UserId);

            if (user == null)
            {
                return BadRequest();
            }

            var result = await userManager.ResetPasswordAsync(user, request.Code, request.Password);

            if (result.Succeeded)
            {
                return Ok();
            }

            return BadRequest();
        }

    }
}
