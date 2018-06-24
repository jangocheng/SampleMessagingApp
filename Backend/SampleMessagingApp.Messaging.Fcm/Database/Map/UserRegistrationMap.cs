using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SampleMessagingApp.Core.Database.Map;
using SampleMessagingApp.Messaging.Fcm.Model;

namespace SampleMessagingApp.Messaging.Fcm.Database.Map
{
    public class UserRegistrationMap : IEntityTypeMap<UserRegistration>
    {
        public void Map(EntityTypeBuilder<UserRegistration> builder)
        {
            builder.ToTable("UserRegistration", "fcm");

            builder
                .HasKey(x => x.Id)
                .HasName("PK_UserRegistration");

            builder.Property(x => x.Id)
                .HasColumnName("Id")
                .ValueGeneratedOnAdd();

            builder
                .HasOne(x => x.User)
                .WithMany()
                .HasForeignKey()
                .HasConstraintName("FK_UserRegistration_User");

            builder.Property(x => x.RegistrationToken)
                .HasColumnName("RegistrationToken")
                .IsRequired();

            builder.Property(x => x.DeactivationDate)
                .HasColumnName("DeactivationDate")
                .IsRequired(false);
        }
    }
}
