using Microsoft.EntityFrameworkCore;
using SampleMessagingApp.Messaging.Fcm.Model;

namespace SampleMessagingApp.Messaging.Fcm.Database.Context
{
    public class FirebaseDbContext : DbContext
    {
        public DbSet<UserRegistration> UserRegistrations { get; set; }
    }
}
