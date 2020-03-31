using System;
using Library.Domain.AggregateModels.BookAggregate.Events;
using Library.Domain.Exceptions;
using Library.Domain.SeedWork;

namespace Library.Domain.AggregateModels.BookAggregate
{
    public class Book : AggregateRoot<long>
    {
        public string Title { get; }
        public string Author { get; }
        public bool IsBorrowed => BorrowedUntil.HasValue;
        public DateTime? BorrowedUntil { get; private set; }
        public long? BorrowedByUserId { get; private set; }

        private Book(string title, string author)
        {
            if (string.IsNullOrWhiteSpace(title))
                throw new BookCreationException($"Parameter {nameof(title)} cannot be empty.");

            if (string.IsNullOrWhiteSpace(author))
                throw new BookCreationException($"Parameter {nameof(author)} cannot be empty.");

            Title = title;
            Author = author;

            BorrowedUntil = null;
            BorrowedByUserId = null;
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

            BorrowedByUserId = userId;
            BorrowedUntil = DateTime.UtcNow.AddDays(daysPeriod);

            AddDomainEvent(new BookBorrowedEvent(Id, BorrowedByUserId.Value, BorrowedUntil.Value));
        }
    }
}
