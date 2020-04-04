using System;
using Library.Domain.AggregateModels.LoanAggregate.Events;
using Library.Domain.Exceptions;
using Library.Domain.SeedWork;

namespace Library.Domain.AggregateModels.LoanAggregate
{
    public class Loan : AggregateRoot<long>
    {
        public static int DefaultLoanPeriodInDays = 30;

        private readonly long _bookId;
        private readonly long _userId;
        private readonly DateTime _endDate;
        private bool _active;
        private Book _book;
        private RegisteredLibraryUser _user;

        public Book Book => _book;
        public RegisteredLibraryUser User => _user;
        public DateTime EndDate => _endDate;
        public bool Active => _active;

        private Loan(long bookId, long userId, DateTime endDate)
        {
            _bookId = bookId;
            _userId = userId;
            _endDate = endDate;
            _active = true;
        }

        public static Loan Create(long bookId, long userId)
        {
            var endDate = DateTime.UtcNow.AddDays(DefaultLoanPeriodInDays);
            var loan = new Loan(bookId, userId, endDate);

            loan.Book.Borrow();

            loan.AddDomainEvent(new LoanCreatedEvent(bookId, userId, endDate));

            return loan;
        }

        public void EndLoan()
        {
            if (!Active)
                throw new LoanNotActiveException(Id);

            _active = false;
            _book.Return();

            AddDomainEvent(new LoanFinishedEvent(Id));
        }
    }
}
