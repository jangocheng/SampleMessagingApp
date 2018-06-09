using System;
using System.Collections.Generic;
using System.Text;

namespace SampleMessagingApp.Core.Web.DTO.Requests
{
    public class RegisterAccountRequest
    {
        public string EMail { get; set; }

        public string Password { get; set; }
    }
}
