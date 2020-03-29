using MediatR;

namespace Library.Domain.AggregateModels.BookAggregate.Events
{
    public class BookCreatedEvent : INotification
    {
        public string Title { get; }
        public string Author { get; }

        public BookCreatedEvent(string title, string author)
        {
            Title = title;
            Author = author;
        }
    }
}
