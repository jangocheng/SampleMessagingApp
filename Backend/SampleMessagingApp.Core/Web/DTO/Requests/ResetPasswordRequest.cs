﻿// Copyright (c) Philipp Wagner. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace SampleMessagingApp.Core.Web.DTO.Requests
{
    public class ResetPasswordRequest
    {
        public string UserId { get; set; }

        public string Code { get; set; }

        public string Password { get; set; }
    }
}
