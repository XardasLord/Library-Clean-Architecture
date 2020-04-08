using MediatR;

namespace Library.Domain.AggregateModels.LibraryUserAggregate.Events
{
    public class LibraryUserCreatedEvent : INotification
    {
        public LibraryUser User { get; }

        public LibraryUserCreatedEvent(LibraryUser user) => User = user;
    }
}
