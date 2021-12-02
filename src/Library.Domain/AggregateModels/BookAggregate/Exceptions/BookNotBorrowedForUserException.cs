using Library.Domain.Exceptions;

namespace Library.Domain.AggregateModels.BookAggregate.Exceptions
{
    public class BookNotBorrowedForUserException : DomainException
    {
        public override string Code => "book_not_borrowed_by_given_user";
        public long BookId { get; }

        public BookNotBorrowedForUserException(long bookId) : base($"Book with ID {bookId} is not borrowed by given user.") 
            => BookId = bookId;
    }
}