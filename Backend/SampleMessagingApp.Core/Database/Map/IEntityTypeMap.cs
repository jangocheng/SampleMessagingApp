using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace SampleMessagingApp.Core.Database.Map
{
    public interface IEntityTypeMap
    {
        void Map(ModelBuilder builder);
    }
}
