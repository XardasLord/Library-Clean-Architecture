using Library.Domain.Exceptions;

namespace Library.Domain.AggregateModels.BookAggregate.Exceptions
{
    public class BookIsAlreadyBorrowedException : DomainException
    {
        public override string Code => "book_is_already_borrowed";

        public BookIsAlreadyBorrowedException() : base($"Book is already borrowed.")
        {
        }
    }
}