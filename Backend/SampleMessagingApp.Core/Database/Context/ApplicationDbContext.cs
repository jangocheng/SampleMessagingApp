﻿// Copyright (c) Philipp Wagner. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System.Collections.Generic;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using SampleMessagingApp.Core.Database.Map;
using SampleMessagingApp.Core.Model.Identity;

namespace SampleMessagingApp.Core.Database.Context
{
    public class EntityTypeMappings
    {
        public readonly IEnumerable<IEntityTypeMap> Mappings;

        public EntityTypeMappings(IEnumerable<IEntityTypeMap> mappings)
        {
            Mappings = mappings;
        }
    }

    public class ApplicationDbContextOptions
    {
        public readonly DbContextOptions<ApplicationDbContext> Options;
        public readonly EntityTypeMappings Mappings;

        public ApplicationDbContextOptions(DbContextOptions<ApplicationDbContext> options, EntityTypeMappings mappings)
        {
            Options = options;
            Mappings = mappings;
        }
    }

    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        private readonly ApplicationDbContextOptions options;
        
        public ApplicationDbContext(ApplicationDbContextOptions options)
            : base(options.Options)
        {
            this.options = options;
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            foreach (var mapping in options.Mappings.Mappings)
            {
                mapping.Map(builder);
            }
        }
    }

    public static class ApplicationDbContextExtensions
    {
        public static DbSet<TEntityType> DbSet<TEntityType>(this ApplicationDbContext context)
            where TEntityType : class
        {
            return context.Set<TEntityType>();
        }
    }
}
