// Copyright (c) Philipp Wagner. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using SampleMessagingApp.Core.Model.Sms;
using Twilio;
using Twilio.Rest.Api.V2010.Account;
using Twilio.Types;

namespace SampleMessagingApp.Core.Services.Sms
{
    public class SmsService : ISmsService
    {
        private readonly SmsOptions options;

        public SmsService(IOptions<SmsOptions> options)
        {
            this.options = options.Value;
        }

        public Task SendSmsAsync(string number, string message)
        {
            var accountSid = options.AccountIdentifier;
            var authToken = options.AccountPassword;

            TwilioClient.Init(accountSid, authToken);

            return MessageResource.CreateAsync(
                to: new PhoneNumber(number),
                from: new PhoneNumber(options.SenderPhoneNumber),
                body: message);
        }
    }
}
