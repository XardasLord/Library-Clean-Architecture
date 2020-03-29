using System;

namespace Library.Domain.Exceptions
{
    public abstract class DomainException : Exception
    {
        protected abstract string Code { get; }

        protected DomainException(string message) : base(message)
        {
        }
    }
}
