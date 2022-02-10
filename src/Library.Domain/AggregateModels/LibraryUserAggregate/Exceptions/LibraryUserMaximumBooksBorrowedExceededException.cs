using Library.Domain.Exceptions;

namespace Library.Domain.AggregateModels.LibraryUserAggregate.Exceptions
{
    public class LibraryUserMaximumBooksBorrowedExceededException : DomainException
    {
        public override string Code => "cannot_borrow_more_books";

        public LibraryUserMaximumBooksBorrowedExceededException()
            : base("Cannot borrow more books at the same time. User has 3 active loans.")
        {
        }
    }
}