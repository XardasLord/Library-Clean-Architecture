using System;
using Library.Domain.AggregateModels.BookAggregate.Events;
using Library.Domain.AggregateModels.BookAggregate.Exceptions;
using Library.Domain.AggregateModels.LibraryUserAggregate;
using Library.Domain.SeedWork;
using Library.Domain.SharedKernel;

namespace Library.Domain.AggregateModels.BookAggregate
{
    public class Book : Entity<long>, IAggregateRoot
    {
        private BookInformation _bookInformation;
        private Loan _currentLoan;

        public BookInformation BookInformation => _bookInformation;
        public bool InStock => _currentLoan is null || !_currentLoan.IsActive;

        private Book() { }

        private Book(BookInformation bookInformation)
        {
            _bookInformation = bookInformation;
            _currentLoan = null; // TODO: Maybe something like Loan.Empty()?
        }

        public static Book Create(string title, string author, string subject, string isbn)
        {
            var bookInformation = new BookInformation(title, author, subject, isbn);
            var book = new Book(bookInformation);

            return book;
        }

        public void Register(LibraryUser libraryUser)
        {
            AddDomainEvent(new NewBookRegisteredEvent(Id, DateTime.UtcNow));
        }
        
        public void Borrow(LibraryUser libraryUser, DateTimePeriod borrowPeriod)
        {
            if (!InStock)
                throw new BookIsNotInStockException();
            
            _currentLoan = Loan.Create(Id, libraryUser.Id, borrowPeriod);

            AddDomainEvent(new BookBorrowedEvent(Id, libraryUser.Id, borrowPeriod));
        }

        public void Return(LibraryUser libraryUser)
        {
            if (InStock)
                throw new BookIsInStockException(Id);
            
            _currentLoan.Finish();

            AddDomainEvent(new BookReturnedEvent(Id, DateTime.UtcNow));
        }
    }
}
