using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.AdditionalFunctionality
{
    public interface ICodeGenerator
    {
        string Generate(int length = 8);
    }
}
