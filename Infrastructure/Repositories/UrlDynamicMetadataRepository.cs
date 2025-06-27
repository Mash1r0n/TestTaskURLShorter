using Application.Interfaces;
using Domain.Entities;
using Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repositories
{
    public class UrlDynamicMetadataRepository : IUrlDynamicMetadataRepository
    {
        private readonly AppDbContext _context;

        public UrlDynamicMetadataRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<UrlDynamicMetadata?> GetByCodeAsync(string code)
        {
            return await _context.ShortUrls
                .Where(x => x.Code == code)
                .Select(x => x.DynamicMetadata)
                .FirstOrDefaultAsync();
        }
    }
}
