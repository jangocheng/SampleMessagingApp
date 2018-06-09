// Copyright (c) Philipp Wagner. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using SampleMessagingApp.Core.Model.Email;

namespace SampleMessagingApp.Core.Services.Email
{
    public class EmailService : IEmailService
    {
        private readonly EmailOptions options;

        public EmailService(IOptions<EmailOptions> options)
        {
            this.options = options.Value;
        }

        public Task SendEmailAsync(string recipient, string subject, string body)
        {
            var message = new MailMessage(options.SenderAddress, recipient, subject, body);

            return SendEmailAsync(message);
        }

        public async Task SendEmailAsync(MailMessage message)
        {
            using (var client = new SmtpClient())
            {
                client.Host = options.SmtpServer;
                client.Port = options.SmtpPort;
                client.EnableSsl = true;
                client.Credentials = new NetworkCredential(options.SmtpUsername, options.SmtpPassword);

                await client.SendMailAsync(message);
            }
        }
    }
}
