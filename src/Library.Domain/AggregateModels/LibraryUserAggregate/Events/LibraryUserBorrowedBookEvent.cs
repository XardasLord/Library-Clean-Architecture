using System;
using MediatR;

namespace Library.Domain.AggregateModels.LibraryUserAggregate.Events
{
    public class LibraryUserBorrowedBookEvent : INotification
    {
        public long BookId { get; }
        public long UserId { get; }
        public DateTime FromDate { get; }
        public DateTime ToDate { get; }

        public LibraryUserBorrowedBookEvent(long bookId, long userId, DateTime fromDate, DateTime toDate)
        {
            BookId = bookId;
            UserId = userId;
            FromDate = fromDate;
            ToDate = toDate;
        }
    }
}
