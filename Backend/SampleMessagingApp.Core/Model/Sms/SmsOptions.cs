// Copyright (c) Philipp Wagner. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace SampleMessagingApp.Core.Model.Sms
{
    public class SmsOptions
    {
        public string AccountIdentifier { get; set; }

        public string AccountPassword { get; set; }

        public string SenderPhoneNumber { get; set; }
    }
}
