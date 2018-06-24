using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using SampleMessagingApp.Messaging.Model;

namespace SampleMessagingApp.Messaging.Database.Context
{
    public class MessagingDbContext : DbContext
    {
        public DbSet<Topic> Topics { get; set; }

        public DbSet<UserRegistration> UserRegistrations { get; set; }

        public DbSet<UserTopic> UserTopics { get; set; }
    }
}
