using System;
using System.Collections.Generic;
using System.Text;

namespace SampleMessagingApp.Core.Web.DTO.Requests
{
    public class ConfirmEmailRequest
    {
        public string UserId { get; set; }

        public string Code { get; set; }
    }
}
