using Library.Domain.Exceptions;

namespace Library.Domain.AggregateModels.LibraryUserAggregate.Exceptions
{
    public class LibraryUserMaximumBooksBorrowedExceededException : DomainException
    {
        public override string Code => "cannot_borrow_more_books";

        public LibraryUserMaximumBooksBorrowedExceededException(string message) : base(message)
        {
        }
    }
}