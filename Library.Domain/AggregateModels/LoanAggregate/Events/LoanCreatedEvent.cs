using System;
using MediatR;

namespace Library.Domain.AggregateModels.LoanAggregate.Events
{
    public class LoanCreatedEvent : INotification
    {
        public long BookId { get; }
        public long UserId { get; }
        public DateTime EndDate { get; }

        public LoanCreatedEvent(long bookId, long userId, DateTime endDate)
        {
            BookId = bookId;
            UserId = userId;
            EndDate = endDate;
        }
    }
}
