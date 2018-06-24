// Copyright (c) Philipp Wagner. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using Microsoft.EntityFrameworkCore;
using SampleMessagingApp.Messaging.Fcm.Model;

namespace SampleMessagingApp.Messaging.Fcm.Database.Context
{
    public class FirebaseDbContext : DbContext
    {
        public DbSet<UserRegistration> UserRegistrations { get; set; }
    }
}
