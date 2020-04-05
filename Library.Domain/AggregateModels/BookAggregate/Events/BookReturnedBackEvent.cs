using System;
using MediatR;

namespace Library.Domain.AggregateModels.BookAggregate.Events
{
    public class BookReturnedBackEvent : INotification
    {
        public long BookId { get; }
        public DateTime GaveBackTime { get; }

        public BookReturnedBackEvent(long bookId, DateTime gaveBackTime)
        {
            BookId = bookId;
            GaveBackTime = gaveBackTime;
            BookId = bookId;
        }
    }
}