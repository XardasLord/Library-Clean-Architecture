namespace Library.Domain.Exceptions
{
    public class BookAlreadyBorrowedException : DomainException
    {
        protected override string Code => "book_is_already_borrowed";

        public BookAlreadyBorrowedException() : base($"Book is already borrowed.")
        {
        }
    }
}
