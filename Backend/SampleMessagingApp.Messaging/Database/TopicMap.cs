using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SampleMessagingApp.Core.Database.Map;
using SampleMessagingApp.Messaging.Model;

namespace SampleMessagingApp.Messaging.Database
{
    public class TopicMap : IEntityTypeMap<Topic>
    {
        public void Map(EntityTypeBuilder<Topic> builder)
        {
            builder.ToTable("Topic", "sample");

            builder.HasKey(x => x.Id)
                .HasName("PK_Topic");

            builder.Property(x => x.Id)
                .HasColumnName("Id")
                .ValueGeneratedOnAdd();

            builder.Property(x => x.Name)
                .HasColumnName("Name")
                .IsRequired()
                .HasMaxLength(255);
        }
    }
}
