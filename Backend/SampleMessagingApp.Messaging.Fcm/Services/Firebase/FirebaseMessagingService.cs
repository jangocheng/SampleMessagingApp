// Copyright (c) Philipp Wagner. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SampleMessagingApp.Core.Database.Context;
using SampleMessagingApp.Core.Database.Factory;
using SampleMessagingApp.Core.Model.Identity;
using SampleMessagingApp.Messaging.Fcm.Model;

namespace SampleMessagingApp.Messaging.Fcm.Services.Firebase
{
    public class FirebaseMessagingService : IFirebaseMessagingService
    {
        private readonly IApplicationDbContextFactory factory;

        public FirebaseMessagingService(IApplicationDbContextFactory factory)
        {
            this.factory = factory;
        }

        public async Task RegisterUserAsync(ApplicationUser user, string registrationToken, CancellationToken cancellationToken = default(CancellationToken))
        {
            using (var context = factory.Create())
            {
                await context.DbSet<UserRegistration>().AddAsync(new UserRegistration {User = user, RegistrationToken = registrationToken}, cancellationToken);
            }
        }

        public async Task UpdateUserRegistrationAsync(ApplicationUser user, string refreshToken, CancellationToken cancellationToken = default(CancellationToken))
        {
            using (var context = factory.Create())
            {
                var registration = await context.DbSet<UserRegistration>()
                    .FirstOrDefaultAsync(x => x.User == user, cancellationToken);

                if (registration == null)
                {
                    throw new Exception($"User {user.Id} is not registered");
                }

                registration.DeactivationDate = DateTime.UtcNow;

                await context.DbSet<UserRegistration>()
                    .AddAsync(new UserRegistration { User = user, RegistrationToken = refreshToken }, cancellationToken);

                await context.SaveChangesAsync(cancellationToken);
            }
        }
    }
}
