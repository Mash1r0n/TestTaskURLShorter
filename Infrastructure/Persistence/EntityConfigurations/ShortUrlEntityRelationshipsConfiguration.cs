using Domain.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Data.SqlClient.Server;

namespace Infrastructure.Persistence.EntityConfigurations
{
    internal sealed class ShortUrlEntityRelationshipsConfiguration : IEntityTypeConfiguration<ShortUrl>
    {
        public void Configure(EntityTypeBuilder<ShortUrl> modelBuilder)
        {
            modelBuilder.HasKey(s => s.Id);

            modelBuilder.Property(s => s.Code).IsRequired();
            modelBuilder.Property(s => s.LongUrl).IsRequired();
            modelBuilder.Property(s => s.CreatedAt).IsRequired();
            modelBuilder.Property(s => s.OwnerId).IsRequired();

            modelBuilder
                .HasOne(s => s.DynamicMetadata)
                .WithOne()
                .HasForeignKey<UrlDynamicMetadata>(m => m.ShortUrlId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
