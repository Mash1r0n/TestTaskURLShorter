using System.Runtime.Serialization;

namespace Domain.Exceptions
{
    public class ValidationNotPassedException : Exception
    {
        public ValidationNotPassedException()
        {
        }

        public ValidationNotPassedException(string? message) : base(message)
        {
        }
    }
}