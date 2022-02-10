using MediatR;

namespace Library.Domain.AggregateModels.LibraryUserAggregate.Events
{
    public class LibraryUserReturnedBookEvent : INotification
    {
        public long LibraryUserId { get; }
        public long BookId { get; }

        public LibraryUserReturnedBookEvent(long libraryUserId, long bookId)
        {
            LibraryUserId = libraryUserId;
            BookId = bookId;
        }
    }
}