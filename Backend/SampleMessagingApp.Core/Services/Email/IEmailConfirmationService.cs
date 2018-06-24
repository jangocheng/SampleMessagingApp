// Copyright (c) Philipp Wagner. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System.Threading.Tasks;
using SampleMessagingApp.Core.Model.Identity;

namespace SampleMessagingApp.Core.Services.Email
{
    public interface IEmailConfirmationService
    {
        Task SendEmailConfirmationAsync(ApplicationUser user, string confirmationUrl);
    }
}