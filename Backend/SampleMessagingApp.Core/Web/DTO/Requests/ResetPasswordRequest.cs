using System;
using System.Collections.Generic;
using System.Text;

namespace SampleMessagingApp.Core.Web.DTO.Requests
{
    public class ResetPasswordRequest
    {
        public string UserId { get; set; }

        public string Code { get; set; }

        public string Password { get; set; }
    }
}
