// Copyright (c) Philipp Wagner. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System.Collections.Generic;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;

namespace SampleMessagingApp.Core.Services.Jwt
{
    public interface IJwtService
    {
        string Audience { get; }

        string ClaimsIssuer { get; }

        SecurityKey SigningKey { get; }

        SigningCredentials SigningCredentials { get; }

        string CreateToken(IEnumerable<Claim> claims);
    }
}