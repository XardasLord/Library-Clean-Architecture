using System;
using MediatR;

namespace Library.Domain.AggregateModels.BookAggregate.Events
{
    public class BookBorrowedEvent : INotification
    {
        public long BookId { get; }
        public DateTime FromDate { get; }
        public DateTime ToDate { get; }

        public BookBorrowedEvent(long bookId, DateTime fromDate, DateTime toDate)
        {
            BookId = bookId;
            FromDate = fromDate;
            ToDate = toDate;
        }
    }
}
