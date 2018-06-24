using System;
using System.Collections.Generic;
using System.Text;
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
