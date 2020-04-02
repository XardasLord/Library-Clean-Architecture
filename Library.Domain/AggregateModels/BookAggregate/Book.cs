using System;
using Library.Domain.AggregateModels.BookAggregate.Events;
using Library.Domain.Exceptions;
using Library.Domain.SeedWork;

namespace Library.Domain.AggregateModels.BookAggregate
{
    public class Book : AggregateRoot<long>
    {
        private readonly string _title;
        private readonly string _author;
        private DateTime? _borrowedUntil;
        private long? _borrowedByUserId;

        public string Title => _title;
        public string Author => _author;
        public bool IsBorrowed => BorrowedUntil.HasValue;
        public DateTime? BorrowedUntil => _borrowedUntil;
        public long? BorrowedByUserId => _borrowedByUserId;

        private Book(string title, string author)
        {
            if (string.IsNullOrWhiteSpace(title))
                throw new BookCreationException($"Parameter {nameof(title)} cannot be empty.");

            if (string.IsNullOrWhiteSpace(author))
                throw new BookCreationException($"Parameter {nameof(author)} cannot be empty.");

            _title = title;
            _author = author;

            _borrowedUntil = null;
            _borrowedByUserId = null;
        }

        public static Book Create(string title, string author)
        {
            var book = new Book(title, author);

            book.AddDomainEvent(new BookCreatedEvent(title, author));

            return book;
        }

        public void Borrow(long userId, uint daysPeriod)
        {
            if (IsBorrowed)
                throw new BookAlreadyBorrowedException(BorrowedUntil.Value);

            if (daysPeriod > 30)
                throw new BookBorrowInvalidPeriodException(daysPeriod);

            _borrowedByUserId = userId;
            _borrowedUntil = DateTime.UtcNow.AddDays(daysPeriod);

            AddDomainEvent(new BookBorrowedEvent(Id, _borrowedByUserId.Value, _borrowedUntil.Value));
        }

        public void GiveBack()
        {
            if (!IsBorrowed)
                throw new BookNotBorrowedException(Id);

            _borrowedByUserId = null;
            _borrowedUntil = null;

            AddDomainEvent(new BookGaveBackEvent(Id, DateTime.UtcNow));
        }
    }
}
