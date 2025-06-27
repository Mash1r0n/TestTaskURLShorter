using Domain.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class UrlDynamicMetadata
    {
        public Guid ShortUrlId { get; private set; }
        public int Clicks { get; private set; }
        public DateTime? LastAccessedAt { get; private set; }

        public void RegisterClick()
        {
            Clicks++;
            LastAccessedAt = DateTime.UtcNow;
        }

        internal UrlDynamicMetadata()
        {
            Clicks = 0;
            LastAccessedAt = null;
        }
    }
}
