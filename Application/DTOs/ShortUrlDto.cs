using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs
{
    public class ShortUrlDto
    {
        public Guid Id { get; private set; }
        public string Code { get; private set; } = null!;
        public string LongUrl { get; private set; } = null!;
        public DateTime CreatedAt { get; private set; }
        public string OwnerId { get; private set; }
    }
}
