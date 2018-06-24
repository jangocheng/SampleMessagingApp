
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SampleMessagingApp.Core.Database.Map;
using SampleMessagingApp.Messaging.Model;

namespace SampleMessagingApp.Messaging.Database
{
    public class UserTopicMap : IEntityTypeMap<UserTopic>
    {
        public void Map(EntityTypeBuilder<UserTopic> builder)
        {
            builder
                .HasKey(x => new { x.Topic, x.User })
                .HasName("PK_UserTopic")
                .ForSqlServerIsClustered();
            
            builder
                .HasOne(x => x.User)
                .WithMany()
                .HasForeignKey()
                .HasConstraintName("FK_UserTopic_User");

            builder
                .HasOne(x => x.Topic)
                .WithMany()
                .HasForeignKey()
                .HasConstraintName("FK_UserTopic_Topic");
        }
    }
}
