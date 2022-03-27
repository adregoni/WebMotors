using System;

namespace WebMotors.Domain.Core.Exceptions
{
    public class DomainNotFoundException : Exception
    {
        public DomainNotFoundException(string message)
              : base(message)
        {
        }
    }
}
