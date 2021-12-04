using System;
using System.Collections.Generic;
using System.Linq;
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
        internal List<Loan> _loans;
        internal bool _inStock;

        public BookInformation BookInformation => _bookInformation;
        public bool InStock => _inStock;

        private Book()
        {
            _loans = new List<Loan>();
        }

        private Book(BookInformation bookInformation) : this()
        {
            _bookInformation = bookInformation;
            _inStock = true;
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
            
            _loans.Add(Loan.Create(Id, libraryUser.Id, borrowPeriod));
            _inStock = false;

            AddDomainEvent(new BookBorrowedEvent(Id, libraryUser.Id, borrowPeriod));
        }

        public void Return(LibraryUser libraryUser)
        {
            if (InStock)
                throw new BookIsInStockException(Id);

            var activeLoan = _loans.Single(l => l.IsActive);
            activeLoan.Finish();

            _inStock = true;

            AddDomainEvent(new BookReturnedEvent(Id, DateTime.UtcNow));
        }
    }
}
