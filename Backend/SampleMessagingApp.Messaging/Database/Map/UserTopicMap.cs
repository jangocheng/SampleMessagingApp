// Copyright (c) Philipp Wagner. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SampleMessagingApp.Core.Database.Map;
using SampleMessagingApp.Messaging.Model;

namespace SampleMessagingApp.Messaging.Database.Map
{
    public class UserTopicMap : BaseEntityMap<UserTopic>
    {
        protected override void InternalMap(EntityTypeBuilder<UserTopic> builder)
        {
            builder
                .ToTable("UserTopic", "messaging");

            builder
                .HasKey("UserId", "TopicId")
                .HasName("PK_UserTopic");

            builder
                .HasOne(x => x.User)
                .WithMany()
                .HasForeignKey("UserId")
                .HasConstraintName("FK_UserTopic_User");

            builder
                .HasOne(x => x.Topic)
                .WithMany()
                .HasForeignKey("TopicId")
                .HasConstraintName("FK_UserTopic_Topic");
        }
    }
}
