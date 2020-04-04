using MediatR;

namespace Library.Domain.AggregateModels.LoanAggregate.Events
{
    public class LoanFinishedEvent : INotification
    {
        public long LoanId { get; }

        public LoanFinishedEvent(long loanId) => LoanId = loanId;
    }
}
