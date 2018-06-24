// Copyright (c) Philipp Wagner. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System.Threading;
using System.Threading.Tasks;
using SampleMessagingApp.Core.Model.Identity;

namespace SampleMessagingApp.Messaging.Fcm.Services
{
    public interface IFirebaseMessagingService
    {
        Task RegisterUserAsync(ApplicationUser user, string registrationToken, CancellationToken cancellationToken = default(CancellationToken));

        Task UpdateUserRegistrationAsync(ApplicationUser user, string refreshToken, CancellationToken cancellationToken = default(CancellationToken));
    }
}
