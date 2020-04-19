namespace Library.Domain.Exceptions
{
    public class BookIsNotInStockException : DomainException
    {
        public override string Code => "book_is_already_borrowed";

        public BookIsNotInStockException() : base($"Book is already borrowed.")
        {
        }
    }
}
