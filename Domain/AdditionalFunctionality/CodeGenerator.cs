using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.AdditionalFunctionality
{
    public class CodeGenerator : ICodeGenerator
    {
        private const string Alphabet = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";

        public string Generate(int length = 8)
        {
            var guidBytes = Guid.NewGuid().ToByteArray();
            var number = BitConverter.ToUInt64(guidBytes, 0);

            var result = new StringBuilder();
            while (result.Length < length)
            {
                result.Append(Alphabet[(int)(number % (ulong)Alphabet.Length)]);
                number /= (ulong)Alphabet.Length;
            }

            return result.ToString();
        }
    }
}
