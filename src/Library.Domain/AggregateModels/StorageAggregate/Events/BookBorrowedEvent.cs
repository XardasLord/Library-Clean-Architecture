using System;
using MediatR;

namespace Library.Domain.AggregateModels.StorageAggregate.Events
{
    public class BookBorrowedEvent : INotification
    {
        public long BookId { get; }
        public long UserId { get; }
        public DateTime FromDate { get; }
        public DateTime ToDate { get; }

        public BookBorrowedEvent(long bookId, long userId, DateTime fromDate, DateTime toDate)
        {
            BookId = bookId;
            UserId = userId;
            FromDate = fromDate;
            ToDate = toDate;
        }
    }
}
