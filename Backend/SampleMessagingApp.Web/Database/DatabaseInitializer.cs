using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using SampleMessagingApp.Core.Database.Context;
using SampleMessagingApp.Core.Model.Identity;
using SampleMessagingApp.Messaging.Fcm.Model;
using SampleMessagingApp.Messaging.Model;

namespace SampleMessagingApp.Web.Database
{
    public static class DatabaseSeedInitializer
    {
        public static async Task InitializeAsync(IConfiguration configuration, UserManager<ApplicationUser> manager, ApplicationDbContext context)
        {
            context.Database.EnsureCreated();

            var admin = new ApplicationUser
            {
                Email = configuration["Users:Admin:Email"],
                UserName = configuration["Users:Admin:UserName"],
                EmailConfirmed = true,
            };

            await manager.CreateAsync(admin, configuration["Users:Admin:Password"]);

            // Add Topics:
            var topic1 = new Topic {Name = "Topic1"};
            var topic2 = new Topic {Name = "Topic2"};

            await context.DbSet<Topic>().AddRangeAsync(topic1, topic2);

            // Add UserTopics:
            var adminTopic1 = new UserTopic {User = admin, Topic = topic1, SubscriptionDate = DateTime.UtcNow};
            var adminTopic2 = new UserTopic {User = admin, Topic = topic1, SubscriptionDate = DateTime.UtcNow};

            await context.DbSet<UserTopic>().AddRangeAsync(adminTopic1, adminTopic2);

            // Add Firebase UserRegistration:
            var adminDevice1 = new UserRegistration {User = admin, RegistrationToken = "Your_Registration_Token"};

            await context.DbSet<UserRegistration>().AddRangeAsync(adminDevice1);

            await context.SaveChangesAsync();
        }
    }
}
