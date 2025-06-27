using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Entities;

namespace Infrastructure.Persistence.EntityConfigurations
{
    internal sealed class UrlDynamicMetadataEntityRelationshipsConfiguration : IEntityTypeConfiguration<UrlDynamicMetadata>
    {
        public void Configure(EntityTypeBuilder<UrlDynamicMetadata> modelBuilder)
        {
            modelBuilder.HasKey(m => m.ShortUrlId);

            modelBuilder.Property(m => m.Clicks).IsRequired();
            modelBuilder.Property(m => m.LastAccessedAt);
        }
    }
}
