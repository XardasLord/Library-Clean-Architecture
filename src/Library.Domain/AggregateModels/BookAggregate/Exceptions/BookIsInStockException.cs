using Library.Domain.Exceptions;

namespace Library.Domain.AggregateModels.BookAggregate.Exceptions
{
    public class BookIsInStockException : DomainException
    {
        public override string Code => "book_is_not_borrowed";
        public long BookId { get; }

        public BookIsInStockException(long bookId) : base($"Book with ID {bookId} is not borrowed.")
            => BookId = bookId;
    }
}