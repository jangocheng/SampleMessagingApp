// Copyright (c) Philipp Wagner. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using SampleMessagingApp.Core.Model.Identity;
using SampleMessagingApp.Core.Services.Jwt;

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
