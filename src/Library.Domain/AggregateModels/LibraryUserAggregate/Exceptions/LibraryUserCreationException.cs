using Library.Domain.Exceptions;

namespace Library.Domain.AggregateModels.LibraryUserAggregate.Exceptions
{
    public class LibraryUserCreationException : DomainException
    {
        public override string Code => "cannot_create_library_user";

        public LibraryUserCreationException(string message) : base(message)
        {
        }
    }
}
