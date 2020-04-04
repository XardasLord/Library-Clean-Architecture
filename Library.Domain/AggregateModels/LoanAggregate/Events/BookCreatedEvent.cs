using MediatR;

namespace Library.Domain.AggregateModels.LoanAggregate.Events
{
    public class BookCreatedEvent : INotification
    {
        public BookInformation BookInformation { get; }

        public BookCreatedEvent(BookInformation bookInformation) 
            => BookInformation = bookInformation;
    }
}
