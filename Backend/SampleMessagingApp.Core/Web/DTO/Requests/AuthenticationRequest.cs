// Copyright (c) Philipp Wagner. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using Newtonsoft.Json;

namespace SampleMessagingApp.Core.Web.DTO.Requests
{
    public class AuthenticationRequest
    {
        [JsonProperty]
        public string Username { get; set; }

        [JsonProperty]
        public string Password { get; set; }
    }

}
