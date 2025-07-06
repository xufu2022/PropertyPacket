using Microsoft.EntityFrameworkCore;
using PropertyPacket.Domain.Catalog;
using System;
using System.Collections.Generic;
using System.Text;

namespace PropertyPacket.Infrastructure
{
    public class PropertyPacketContext(DbContextOptions<PropertyPacketContext> options) : DbContext(options)
    {
        public Category Categories { get; set; } = null!;
        public CategoryTemplate CategoriesTemplate { get; set; } = null!;
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(PropertyPacketContext).Assembly);
            base.OnModelCreating(modelBuilder);
        }
    }
}
