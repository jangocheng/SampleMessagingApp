// Copyright (c) Philipp Wagner. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using SampleMessagingApp.Core.Model.Identity;

namespace SampleMessagingApp.Messaging.Fcm.Model
{
    public class UserRegistration
    {
        public int Id { get; set; }

        public ApplicationUser User { get; set; }

        public string RegistrationToken { get; set; }

        public DateTime? DeactivationDate { get; set; }
    }
}
