using MediatR;

namespace Library.Domain.AggregateModels.BookAggregate.Events
{
    public class BookBorrowedEvent : INotification
    {
        public long BookId { get; }

        public BookBorrowedEvent(long bookId) => BookId = bookId;
    }
}
