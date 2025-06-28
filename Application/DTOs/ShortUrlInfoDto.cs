using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs
{
    public class ShortUrlInfoDto
    {
        public DateTime CreatedAt { get; private set; }
        public string OwnerId { get; private set; }
        public int Clicks { get; private set; }
        public DateTime? LastAccessedAt { get; private set; }
    }
}