using MediatR;

namespace Library.Domain.AggregateModels.LoanAggregate.Events
{
    public class LoanEndedEvent : INotification
    {
        public long LoanId { get; }

        public LoanEndedEvent(long loanId) => LoanId = loanId;
    }
}
