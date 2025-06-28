using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces.Repositories
{
    public interface IShortUrlRepository
    {
        Task<ShortUrl?> GetByCodeAsync(string code);
        Task<ShortUrl?> GetByIdAsync(Guid id);
        Task<List<ShortUrl>> GetAllShortUrlsAsync();
        Task AddAsync(ShortUrl shortUrl);
        Task UpdateAsync(ShortUrl shortUrl);
        Task DeleteAsync(ShortUrl shortUrl);
        Task<bool> ExistsForUserAsync(string longUrl, string ownerId);
    }
}
