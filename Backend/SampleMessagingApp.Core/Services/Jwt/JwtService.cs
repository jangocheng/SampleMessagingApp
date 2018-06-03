// Copyright (c) Philipp Wagner. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;

namespace SampleMessagingApp.Core.Services.Jwt
{
    public class JwtService : IJwtService
    {
        private readonly string claimsIssuer;

        private readonly string audience;

        private readonly SecurityKey signingKey;
        
        private readonly SigningCredentials signingCredentials;

        public JwtService(string claimsIssuer, string audience, SecurityKey signingKey, SigningCredentials signingCredentials)
        {
            this.claimsIssuer = claimsIssuer;
            this.audience = audience;
            this.signingCredentials = signingCredentials;
            this.signingKey = signingKey;
        }

        public string Audience => audience;

        public string ClaimsIssuer => claimsIssuer;

        public SecurityKey SigningKey => signingKey;

        public SigningCredentials SigningCredentials => signingCredentials;

        public string CreateToken(IEnumerable<Claim> claims)
        {
            var token = new JwtSecurityToken(issuer: ClaimsIssuer,
                signingCredentials: SigningCredentials,
                audience: Audience,
                claims: claims);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }

}
