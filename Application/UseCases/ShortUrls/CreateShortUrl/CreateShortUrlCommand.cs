using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.UseCases.ShortUrls.CreateShortUrl
{
    public class CreateShortUrlCommand
    {
        public string LongUrl { get; set; } = null!;
        public string OwnerId { get; set; } = null!;
    }
}
