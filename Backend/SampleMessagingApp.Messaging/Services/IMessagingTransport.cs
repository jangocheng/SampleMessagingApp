// Copyright (c) Philipp Wagner. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System.Threading;
using System.Threading.Tasks;
using SampleMessagingApp.Core.Model.Identity;
using SampleMessagingApp.Messaging.Model;

namespace SampleMessagingApp.Messaging.Services
{
    public interface IMessagingTransport
    {
        Task SubscribeAsync(ApplicationUser user, Topic topic, CancellationToken cancellationToken = default(CancellationToken));

        Task SendNotificationAsync(Topic topic, Notification notification, CancellationToken cancellationToken = default(CancellationToken));

        Task SendNotificationAsync(ApplicationUser user, Notification notification, CancellationToken cancellationToken = default(CancellationToken));
    }
}
