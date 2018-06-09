using System;
using System.Collections.Generic;
using System.Text;

namespace SampleMessagingApp.Core.Model.Sms
{
    public class SmsOptions
    {
        public string AccountIdentifier { get; set; }

        public string AccountPassword { get; set; }

        public string SenderPhoneNumber { get; set; }
    }
}
