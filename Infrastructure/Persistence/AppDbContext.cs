using Domain.Entities;
using Infrastructure.Persistence.EntityConfigurations;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Persistence
{
    public class AppDbContext : IdentityDbContext
    {
        public AppDbContext()
            : base()
        {

        }

        public AppDbContext(DbContextOptions options)
            : base(options)
        {

        }

        public DbSet<ShortUrl> ShortUrls { get; set; }
        public DbSet<UrlDynamicMetadata> UrlDynamicMetadatas { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=UrlShorterTestTaskDB;Integrated Security=SSPI");
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            ArgumentNullException.ThrowIfNull(builder);

            base.OnModelCreating(builder);

            builder.ApplyConfigurationsFromAssembly(typeof(ShortUrlEntityRelationshipsConfiguration).Assembly);
        }
    }
}
