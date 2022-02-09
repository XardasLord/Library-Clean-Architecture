using Library.Domain.Exceptions;

namespace Library.Domain.AggregateModels.LibraryUserAggregate.Exceptions
{
    public class LibraryUserDoesNotHaveBookBorrowed : DomainException
    {
        public override string Code => "user_does_not_have_this_book_borrowed";

        public LibraryUserDoesNotHaveBookBorrowed(long bookId)
            : base($"User does not have this book borrowed in the system.")
        {
        }
    }
}