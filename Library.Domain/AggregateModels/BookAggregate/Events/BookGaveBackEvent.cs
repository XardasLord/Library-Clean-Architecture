using System;
using MediatR;

namespace Library.Domain.AggregateModels.BookAggregate.Events
{
    public class BookGaveBackEvent : INotification
    {
        public long BookId { get; }
        public DateTime GaveBackTime { get; }

        public BookGaveBackEvent(long bookId, DateTime gaveBackTime)
        {
            BookId = bookId;
            GaveBackTime = gaveBackTime;
            BookId = bookId;
        }
    }
}