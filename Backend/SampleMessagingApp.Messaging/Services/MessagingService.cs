using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SampleMessagingApp.Core.Model.Identity;
using SampleMessagingApp.Messaging.Database.Context;
using SampleMessagingApp.Messaging.Exceptions;
using SampleMessagingApp.Messaging.Model;

namespace SampleMessagingApp.Messaging.Services
{
    public class MessagingService : IMessagingService
    {
        private readonly IMessagingTransport transport;

        public MessagingService(IMessagingTransport transport)
        {
            this.transport = transport;
        }
        
        public async Task SubscribeAsync(ApplicationUser user, Topic topic, CancellationToken cancellationToken = default(CancellationToken))
        {
            await transport.SubscribeAsync(user, topic, cancellationToken);

            using (var context = new MessagingDbContext())
            {
                await context.UserTopics.AddAsync(new UserTopic() {User = user, Topic = topic, SubscriptionDate = DateTime.UtcNow}, cancellationToken);

                await context.SaveChangesAsync(cancellationToken);
            }
        }

        public async Task UnsubscribeAsync(ApplicationUser user, Topic topic, CancellationToken cancellationToken = default(CancellationToken))
        {
            using (var context = new MessagingDbContext())
            {
                var registration = await context.UserTopics.FirstOrDefaultAsync(x => x.User == user && x.Topic == topic, cancellationToken);

                if (registration == null)
                {
                    throw new UserTopicNotRegisteredException();
                }

                registration.UnsubscriptionDate = DateTime.UtcNow;

                await context.SaveChangesAsync(cancellationToken);
            }
        }

        public async Task SendNotificationAsync(Topic topic, Notification notification, CancellationToken cancellationToken = default(CancellationToken))
        {
            await transport.SendNotificationAsync(topic, notification, cancellationToken);
        }

        public async Task SendNotificationAsync(ApplicationUser user, Notification notification, CancellationToken cancellationToken = default(CancellationToken))
        {
            await transport.SendNotificationAsync(user, notification, cancellationToken);
        }


        public async Task<IList<Topic>> GetTopicsAsync(ApplicationUser user, CancellationToken cancellationToken = default(CancellationToken))
        {
            using (var context = new MessagingDbContext())
            {
                var registrations = await context.UserTopics
                        // We need the Topics:
                        .Include(x => x.Topic)
                    // Where the User is the given user:
                    .Where(x => x.User == user)
                    // And there is an active registration:
                    .Where(x => x.SubscriptionDate <= DateTime.UtcNow && x.UnsubscriptionDate > DateTime.UtcNow)
                    // Select the Topics:
                    .Select(x => x.Topic)
                    // And evaluate it:
                    .ToListAsync(cancellationToken);

                return registrations;
            }
        }
    }
}