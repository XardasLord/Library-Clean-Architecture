using Library.Domain.AggregateModels.BookAggregate;
using MediatR;

namespace Library.Domain.AggregateModels.LibraryUserAggregate.Events
{
    public class LibraryUserReturnedBookEvent : INotification
    {
        public LibraryUserId LibraryUserId { get; }
        public BookId BookId { get; }

        public LibraryUserReturnedBookEvent(LibraryUserId libraryUserId, BookId bookId)
        {
            LibraryUserId = libraryUserId;
            BookId = bookId;
        }
    }
}