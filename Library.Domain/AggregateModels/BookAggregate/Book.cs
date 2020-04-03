using System;
using Library.Domain.AggregateModels.BookAggregate.Events;
using Library.Domain.Exceptions;
using Library.Domain.SeedWork;

namespace Library.Domain.AggregateModels.BookAggregate
{
    public class Book : AggregateRoot<long>
    {
        public static int MaximumLoanPeriodInDays = 30;

        private BookInformation _bookInformation;
        private DateTime? _loanUntil;
        private long? _borrowedByUserId;

        public BookInformation BookInformation => _bookInformation;
        public bool InStock => !LoanUntil.HasValue;
        public DateTime? LoanUntil => _loanUntil;
        public long? BorrowedByUserId => _borrowedByUserId;

        private Book(BookInformation bookInformation)
        {
            _bookInformation = bookInformation;
            _loanUntil = null;
            _borrowedByUserId = null;
        }

        public static Book Create(BookInformation bookInformation)
        {
            var book = new Book(bookInformation);

            book.AddDomainEvent(new BookCreatedEvent(bookInformation));

            return book;
        }

        public void Borrow(long userId, int requestedLoanPeriodInDays)
        {
            if (!InStock)
                throw new BookAlreadyBorrowedException(LoanUntil.Value);

            if (requestedLoanPeriodInDays > MaximumLoanPeriodInDays || requestedLoanPeriodInDays <= 0)
                throw new BookBorrowInvalidPeriodException(requestedLoanPeriodInDays, MaximumLoanPeriodInDays);

            _borrowedByUserId = userId;
            _loanUntil = DateTime.UtcNow.AddDays(requestedLoanPeriodInDays);

            AddDomainEvent(new BookBorrowedEvent(Id, _borrowedByUserId.Value, _loanUntil.Value));
        }

        public void ReturnBack()
        {
            if (InStock)
                throw new BookNotBorrowedException(Id);

            _borrowedByUserId = null;
            _loanUntil = null;

            AddDomainEvent(new BookReturnedBackEvent(Id, DateTime.UtcNow));
        }
    }
}
