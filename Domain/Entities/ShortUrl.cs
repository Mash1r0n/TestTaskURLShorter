using Domain.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class ShortUrl
    {
        public Guid Id { get; private set; }
        public string Code { get; private set; } = null!;
        public string LongUrl { get; private set; } = null!;
        public DateTime CreatedAt { get; private set; }
        public string OwnerId { get; private set; }
        public UrlDynamicMetadata DynamicMetadata { get; private set; } = null!;

        public void ChangeDestination(string newUrl)
        {
            if (!Uri.IsWellFormedUriString(newUrl, UriKind.Absolute)) throw new ValidationNotPassedException($"Invalid URL: {nameof(newUrl)}");

            LongUrl = newUrl;
        }

        public static ShortUrl Create(string longUrl, string ownerId, string shortedUrlCode)
        {
            if (!Uri.IsWellFormedUriString(longUrl, UriKind.Absolute)) throw new ValidationNotPassedException($"Invalid URL: {nameof(longUrl)}");

            return new ShortUrl
            {
                Id = Guid.NewGuid(),
                Code = shortedUrlCode,
                CreatedAt = DateTime.UtcNow,
                LongUrl = longUrl,
                OwnerId = ownerId,
                DynamicMetadata = new UrlDynamicMetadata()
            };
        }

        public void RegisterClick()
        {
            DynamicMetadata.RegisterClick();
        }

        private ShortUrl() { }
    }
}
