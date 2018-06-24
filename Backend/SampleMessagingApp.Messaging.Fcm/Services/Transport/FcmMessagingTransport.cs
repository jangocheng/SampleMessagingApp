// Copyright (c) Philipp Wagner. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using FcmSharp;
using FcmSharp.Requests;
using Microsoft.EntityFrameworkCore;
using SampleMessagingApp.Core.Model.Identity;
using SampleMessagingApp.Messaging.Fcm.Database.Context;
using SampleMessagingApp.Messaging.Model;
using SampleMessagingApp.Messaging.Services;
using Notification = SampleMessagingApp.Messaging.Model.Notification;

namespace SampleMessagingApp.Messaging.Fcm.Services.Transport
{
    public class FcmMessagingTransport : IMessagingTransport, IDisposable
    {
        private readonly IFcmClient client;

        public FcmMessagingTransport(IFcmClient client)
        {
            this.client = client;
        }
        
        public async Task SubscribeAsync(ApplicationUser user, Topic topic, CancellationToken cancellationToken = default(CancellationToken))
        {
            using (var context = new FirebaseDbContext())
            {
                var registrationTokens = await context.UserRegistrations
                    .Where(x => x.User == user)
                    .Where(x => x.DeactivationDate == null)
                    .Select(x => x.RegistrationToken)
                    .ToArrayAsync(cancellationToken);

                var topicManagementRequest = new TopicManagementRequest
                {
                    Topic = topic.Name,
                    RegistrationTokens = registrationTokens
                };

                await client.SubscribeToTopic(topicManagementRequest, cancellationToken);
            }
        }

        public async Task SendNotificationAsync(Topic topic, Notification notification, CancellationToken cancellationToken = default(CancellationToken))
        {
            var message = new FcmMessage
            {
                ValidateOnly = false,
                Message = new Message
                {
                    Topic = topic.Name,
                    Notification = ConvertNotification(notification)
                }
            };

            await client.SendAsync(message, cancellationToken);
        }

        public async Task SendNotificationAsync(ApplicationUser user, Notification notification, CancellationToken cancellationToken = default(CancellationToken))
        {
            using (var context = new FirebaseDbContext())
            {
                var tokens = await context.UserRegistrations
                    .Where(x => x.DeactivationDate == null)
                    .Select(x => x.RegistrationToken)
                    .ToListAsync(cancellationToken);

                var tasks = tokens.Select(x => SendNotificationAsync(x, notification, cancellationToken));

                await Task.WhenAll(tasks);
            }
        }

        private async Task SendNotificationAsync(string token, Notification notification, CancellationToken cancellationToken)
        {

            var message = new FcmMessage
            {
                ValidateOnly = false,
                Message = new Message
                {
                    Token = token,
                    Notification = ConvertNotification(notification)
                }
            };

            await client.SendAsync(message, cancellationToken);
        }

        private FcmSharp.Requests.Notification ConvertNotification(Notification source)
        {
            if (source == null)
            {
                return null;
            }

            return new FcmSharp.Requests.Notification
            {
                Title = source.Title,
                Body = source.Body
            };
        }

        public void Dispose()
        {
            client?.Dispose();
        }
    }
}
