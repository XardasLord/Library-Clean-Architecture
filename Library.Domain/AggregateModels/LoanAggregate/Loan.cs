using System;
using System.Threading.Tasks;
using Library.Domain.AggregateModels.BookAggregate;
using Library.Domain.AggregateModels.LibraryUserAggregate;
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

        public DateTime EndDate => _endDate;
        public bool Active => _active;

        private Loan(long bookId, long userId, DateTime endDate)
        {
            _bookId = bookId;
            _userId = userId;
            _endDate = endDate;
            _active = true;
        }

        public static Loan Create(Book book, LibraryUser user)
        {
            book.Borrow();

            var endDate = DateTime.UtcNow.AddDays(DefaultLoanPeriodInDays);
            var loan = new Loan(book.Id, user.Id, endDate);

            loan.AddDomainEvent(new LoanCreatedEvent(book.Id, user.Id, endDate));

            return loan;
        }

        public async Task EndLoan(IBookRepository bookRepository)
        {
            if (!Active)
                throw new LoanNotActiveException(Id);

            _active = false;

            var book = await bookRepository.GetAsync(_bookId);
            book.Return();

            AddDomainEvent(new LoanEndedEvent(Id));
        }
    }
}
