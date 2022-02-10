using Library.Domain.SharedKernel;
using MediatR;

namespace Library.Domain.AggregateModels.BookAggregate.Events
{
    public class BookBorrowedEvent : INotification
    {
        public long BookId { get; }
        public long UserId { get; }
        public DateTimePeriod DateTimePeriod { get; }

        public BookBorrowedEvent(long bookId, long userId, DateTimePeriod dateTimePeriod)
        {
            BookId = bookId;
            UserId = userId;
            DateTimePeriod = dateTimePeriod;
        }
    }
}
