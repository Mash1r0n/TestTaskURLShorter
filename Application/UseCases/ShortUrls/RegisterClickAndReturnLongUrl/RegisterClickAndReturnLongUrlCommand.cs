using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.UseCases.ShortUrls.RegisterClickAndReturnLongUrl
{
    public class RegisterClickAndReturnLongUrlCommand
    {
        public string code { get; set; } = null!;
    }
}
