using Library.Domain.Exceptions;

namespace Library.Domain.AggregateModels.BookAggregate.Exceptions
{
    public class BookCreationException : DomainException
    {
        public override string Code => "cannot_create_book";

        public BookCreationException(string message) : base(message)
        {
        }
    }
}
