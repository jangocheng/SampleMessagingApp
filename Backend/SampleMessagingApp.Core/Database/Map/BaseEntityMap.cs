using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace SampleMessagingApp.Core.Database.Map
{
    public abstract class BaseEntityMap<TEntityType> : IEntityTypeMap
        where TEntityType : class
    {
        public void Map(ModelBuilder builder)
        {
            InternalMap(builder.Entity<TEntityType>());
        }

        protected abstract void InternalMap(EntityTypeBuilder<TEntityType> builder);
    }
}
