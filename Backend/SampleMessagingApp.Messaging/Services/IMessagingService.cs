// Copyright (c) Philipp Wagner. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using SampleMessagingApp.Core.Model.Identity;
using SampleMessagingApp.Messaging.Model;

namespace SampleMessagingApp.Messaging.Services
{
    public interface IMessagingService
    {
        Task SubscribeAsync(ApplicationUser user, Topic topic, CancellationToken cancellationToken = default(CancellationToken));

        Task UnsubscribeAsync(ApplicationUser user, Topic topic, CancellationToken cancellationToken = default(CancellationToken));

        Task<IList<Topic>> GetTopicsAsync(ApplicationUser user, CancellationToken cancellationToken = default(CancellationToken));
    }
}
