// Copyright (c) Philipp Wagner. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using SampleMessagingApp.Core.Model.Identity;

namespace SampleMessagingApp.Messaging.Model
{
    public class UserTopic
    {
        public ApplicationUser User { get; set; }

        public Topic Topic { get; set; }

        public DateTime SubscriptionDate { get; set; }

        public DateTime? UnsubscriptionDate { get; set; }
    }
}
