using Library.Domain.AggregateModels.LibraryUserAggregate;
using Library.Domain.Exceptions;
using Library.Domain.SeedWork;

namespace Library.Domain.AggregateModels.StorageAggregate
{
    public class Loan : Entity<long>
    {
        private readonly Book _book;
        private readonly long _bookId;
        private readonly LibraryUser _user;
        private readonly long _userId;
        private readonly DateTimePeriod _dateTimePeriod;
        private bool _active;

        public long BookId => _bookId;
        public long UserId => _userId;
        public DateTimePeriod DateTimePeriod => _dateTimePeriod;
        public bool Active => _active;

        private Loan() { }

        private Loan(long bookId, long userId, DateTimePeriod dateTimePeriod)
        {
            _bookId = bookId;
            _userId = userId;
            _dateTimePeriod = dateTimePeriod;
            _active = true;
        }

        public static Loan Create(long bookId, long userId, DateTimePeriod dateTimePeriod)
        {
            var loan = new Loan(bookId, userId, dateTimePeriod);

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
