using System;
using Library.Domain.AggregateModels.BookAggregate.Events;
using Library.Domain.Exceptions;
using Library.Domain.SeedWork;

namespace Library.Domain.AggregateModels.BookAggregate
{
    public class Book : AggregateRoot<long>
    {
        public static int MaximumLoanPeriodInDays = 30;

        private readonly string _title;
        private readonly string _author;
        private DateTime? _loanUntil;
        private long? _borrowedByUserId;

        public string Title => _title;
        public string Author => _author;
        public bool IsBorrowed => LoanUntil.HasValue;
        public DateTime? LoanUntil => _loanUntil;
        public long? BorrowedByUserId => _borrowedByUserId;

        private Book(string title, string author)
        {
            if (string.IsNullOrWhiteSpace(title))
                throw new BookCreationException($"Parameter {nameof(title)} cannot be empty.");

            if (string.IsNullOrWhiteSpace(author))
                throw new BookCreationException($"Parameter {nameof(author)} cannot be empty.");

            _title = title;
            _author = author;

            _loanUntil = null;
            _borrowedByUserId = null;
        }

        public static Book Create(string title, string author)
        {
            var book = new Book(title, author);

            book.AddDomainEvent(new BookCreatedEvent(title, author));

            return book;
        }

        public void Borrow(long userId, int requestedLoanPeriodInDays)
        {
            if (IsBorrowed)
                throw new BookAlreadyBorrowedException(LoanUntil.Value);

            if (requestedLoanPeriodInDays > MaximumLoanPeriodInDays || requestedLoanPeriodInDays <= 0)
                throw new BookBorrowInvalidPeriodException(requestedLoanPeriodInDays, MaximumLoanPeriodInDays);

            _borrowedByUserId = userId;
            _loanUntil = DateTime.UtcNow.AddDays(requestedLoanPeriodInDays);

            AddDomainEvent(new BookBorrowedEvent(Id, _borrowedByUserId.Value, _loanUntil.Value));
        }

        public void ReturnBack()
        {
            if (!IsBorrowed)
                throw new BookNotBorrowedException(Id);

            _borrowedByUserId = null;
            _loanUntil = null;

            AddDomainEvent(new BookReturnedBackEvent(Id, DateTime.UtcNow));
        }
    }
}
