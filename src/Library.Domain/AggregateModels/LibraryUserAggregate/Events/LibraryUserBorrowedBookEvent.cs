using MediatR;

namespace Library.Domain.AggregateModels.LibraryUserAggregate.Events
{
    public class LibraryUserBorrowedBookEvent : INotification
    {
        public long LibraryUserId { get; }
        public long BookId { get; }

        public LibraryUserBorrowedBookEvent(long libraryUserId, long bookId)
        {
            LibraryUserId = libraryUserId;
            BookId = bookId;
        }
    }
}