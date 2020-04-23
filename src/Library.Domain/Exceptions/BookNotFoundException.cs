namespace Library.Domain.Exceptions
{
    public class BookNotFoundException : DomainException
    {
        public override string Code => "book_not_found";
        public long BookId { get; }

        public BookNotFoundException(long bookId) : base($"Book with ID {bookId} was not found.") 
            => BookId = bookId;
    }
}
