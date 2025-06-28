using Application.Interfaces.Repositories;
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
    public class ShortUrlRepository : IShortUrlRepository
    {
        private readonly AppDbContext _context;

        public ShortUrlRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task AddAsync(ShortUrl shortUrl)
        {
            _context.ShortUrls.Add(shortUrl);

            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(ShortUrl shortUrl)
        {
            _context.ShortUrls.Remove(shortUrl);

            await _context.SaveChangesAsync();
        }

        public async Task<bool> ExistsForUserAsync(string longUrl, string ownerId)
        {
            return await _context.ShortUrls.AnyAsync(x => x.LongUrl == longUrl && x.OwnerId == ownerId);
        }

        public async Task<List<ShortUrl>> GetAllShortUrlsAsync()
        {
            return await _context.ShortUrls
                .Include(x => x.DynamicMetadata)
                .ToListAsync();
        }

        public async Task<ShortUrl?> GetByCodeAsync(string code)
        {
            return await _context.ShortUrls
                .Include(x => x.DynamicMetadata)
                .FirstOrDefaultAsync(x => x.Code == code);
        }

        public async Task<ShortUrl?> GetByIdAsync(Guid id)
        {
            return await _context.ShortUrls
                .Include(x => x.DynamicMetadata)
                .FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task UpdateAsync(ShortUrl shortUrl)
        {
            _context.ShortUrls.Update(shortUrl);

            await _context.SaveChangesAsync();
        }
    }
}
