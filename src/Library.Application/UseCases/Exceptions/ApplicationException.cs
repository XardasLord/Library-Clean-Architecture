using System;

namespace Library.Application.UseCases.Exceptions
{
    public abstract class ApplicationException : Exception
    {
        protected abstract string Code { get; }

        protected ApplicationException(string message) : base(message)
        {
        }
    }
}
