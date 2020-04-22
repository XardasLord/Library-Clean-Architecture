namespace Library.Domain.Exceptions
{
    public class BookAlreadyExistsInStorageException : DomainException
    {
        public override string Code => "book_already_exists_in_the_storage";
        public long BookId { get; }

        public BookAlreadyExistsInStorageException(long bookId) : base($"Book with ID {bookId} already exists in the storage.") 
            => BookId = bookId;
    }
}
