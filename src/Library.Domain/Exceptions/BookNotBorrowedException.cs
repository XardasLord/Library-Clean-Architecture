namespace Library.Domain.Exceptions
{
    public class BookNotBorrowedException : DomainException
    {
        public override string Code => "book_is_not_borrowed";
        public long BookId { get; }

        public BookNotBorrowedException(long bookId) : base($"Book with ID {bookId} is not borrowed.")
            => BookId = bookId;
    }
}