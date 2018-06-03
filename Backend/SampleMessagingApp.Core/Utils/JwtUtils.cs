// Copyright (c) Philipp Wagner. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System.Text;
using Microsoft.IdentityModel.Tokens;

namespace SampleMessagingApp.Core.Utils
{
    public static class JwtUtils
    {
        public static SymmetricSecurityKey GetSymmetricSecurityKey(string secretKey)
        {
            return new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey));
        }

        public static SigningCredentials GetSigningCredentials(string secretKey, string securityAlgorithm)
        {
            SymmetricSecurityKey symmetricSecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey));
            
            return new SigningCredentials(symmetricSecurityKey, securityAlgorithm);
        }
    }
}