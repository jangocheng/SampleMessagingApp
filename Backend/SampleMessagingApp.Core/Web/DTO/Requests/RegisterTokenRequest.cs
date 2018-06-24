// Copyright (c) Philipp Wagner. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using Newtonsoft.Json;

namespace SampleMessagingApp.Core.Web.DTO.Requests
{
    public class RegisterTokenRequest
    {
        [JsonProperty("iid")]
        public string InstanceId { get; set; }

        [JsonProperty("token")]
        public string Token  { get; set; }
    }
}
