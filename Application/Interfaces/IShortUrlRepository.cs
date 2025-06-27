using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface IShortUrlRepository
    {
        Task<ShortUrl?> GetByCodeAsync(string code);
        Task AddAsync(ShortUrl shortUrl);
        Task UpdateAsync(ShortUrl shortUrl);
        Task<bool> ExistsForUserAsync(string longUrl, string ownerId);
    }
}
