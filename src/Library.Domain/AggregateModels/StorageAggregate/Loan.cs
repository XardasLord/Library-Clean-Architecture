using System;
using Library.Domain.Exceptions;
using Library.Domain.SeedWork;

namespace Library.Domain.AggregateModels.StorageAggregate
{
    public class Loan : Entity<long>
    {
        public static int DefaultLoanPeriodInDays = 30;

        private readonly long _bookId;
        private readonly long _userId;
        private readonly DateTime _endDate;
        private bool _active;

        public long BookId => _bookId;
        public long UserId => _userId;
        public DateTime EndDate => _endDate;
        public bool Active => _active;

        private Loan() { }

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

            return loan;
        }

        public void EndLoan()
        {
            if (!_active)
                throw new LoanNotActiveException(Id);

            _active = false;
        }
    }
}
