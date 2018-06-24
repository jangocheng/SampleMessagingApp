// Copyright (c) Philipp Wagner. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SampleMessagingApp.Core.Database.Map;
using SampleMessagingApp.Messaging.Model;

namespace SampleMessagingApp.Messaging.Database.Map
{
    public class TopicMap : BaseEntityMap<Topic>
    {
        protected override void InternalMap(EntityTypeBuilder<Topic> builder)
        {
            builder.ToTable("Topic", "messaging");

            builder
                .HasKey(x => x.Id)
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
