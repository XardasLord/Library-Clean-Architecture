using System;
using MediatR;

namespace Library.Domain.AggregateModels.BookAggregate.Events
{
    public class BookBorrowedEvent : INotification
    {
        public long BookId { get; }
        public long UserId { get; }
        public DateTime BorrowedUntil { get; }

        public BookBorrowedEvent(long bookId, long userId, DateTime borrowedUntil)
        {
            BookId = bookId;
            UserId = userId;
            BorrowedUntil = borrowedUntil;
        }
    }
}
