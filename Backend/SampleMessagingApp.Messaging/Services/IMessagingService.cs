using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using SampleMessagingApp.Core.Model.Identity;
using SampleMessagingApp.Messaging.Model;

namespace SampleMessagingApp.Messaging.Services
{
    public interface IMessagingService
    {
        Task RegisterUserAsync(ApplicationUser user, string registrationToken, CancellationToken cancellationToken = default(CancellationToken));

        Task UpdateUserRegistrationAsync(ApplicationUser user, string refreshToken, CancellationToken cancellationToken = default(CancellationToken));

        Task SubscribeAsync(ApplicationUser user, Topic topic, CancellationToken cancellationToken = default(CancellationToken));

        Task UnsubscribeAsync(ApplicationUser user, Topic topic, CancellationToken cancellationToken = default(CancellationToken));

        Task<IList<Topic>> GetTopicsAsync(ApplicationUser user, CancellationToken cancellationToken = default(CancellationToken));
    }
}
