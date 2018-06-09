// Copyright (c) Philipp Wagner. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System.Net.Mail;
using System.Threading.Tasks;

namespace SampleMessagingApp.Core.Services.Email
{
    public interface IEmailService
    {
        Task SendEmailAsync(string email, string subject, string message);

        Task SendEmailAsync(MailMessage message);
    }
}
