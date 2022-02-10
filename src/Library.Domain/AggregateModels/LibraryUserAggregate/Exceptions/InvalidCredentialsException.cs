using Library.Domain.Exceptions;

namespace Library.Domain.AggregateModels.LibraryUserAggregate.Exceptions
{
    public class InvalidCredentialsException : DomainException
    {
        public override string Code => "invalid_credentials";

        public InvalidCredentialsException(string message) : base(message)
        {
        }
    }
}