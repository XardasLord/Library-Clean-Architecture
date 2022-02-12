using Library.Application.UseCases.Exceptions;

namespace Library.Application.UseCases.Books.Exceptions
{
    public class BookNotAvailableException : ApplicationException
    {
        public long BookId { get; }
        public override string Code => "book_is_already_borrowed";
        

        public BookNotAvailableException(long bookId) : base($"Book is already borrowed.")
        {
            BookId = bookId;
        }
    }
}