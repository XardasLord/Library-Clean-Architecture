using Library.Domain.SharedKernel;
using MediatR;

namespace Library.Domain.AggregateModels.LibraryUserAggregate.Events
{
    public class LibraryUserBorrowedBookEvent : INotification
    {
        public long LibraryUserId { get; }
        public long BookId { get; }
        public DateTimePeriod BorrowPeriod { get; }

        public LibraryUserBorrowedBookEvent(long libraryUserId, long bookId, DateTimePeriod borrowPeriod)
        {
            LibraryUserId = libraryUserId;
            BookId = bookId;
            BorrowPeriod = borrowPeriod;
        }
    }
}