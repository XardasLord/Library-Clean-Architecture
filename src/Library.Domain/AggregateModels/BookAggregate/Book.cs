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
        internal BookInformation _bookInformation;
        internal List<Loan> _loans;
        internal bool _inStock;

        public BookInformation BookInformation => _bookInformation;
        public bool InStock => _inStock;

        internal Book()
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
        
        public void SetAsNotAvailable(long libraryUserId, DateTimePeriod borrowPeriod)
        {
            if (!InStock)
                throw new BookIsNotInStockException();
            
            _inStock = false;

            AddDomainEvent(new BookBorrowedEvent(Id, libraryUserId, borrowPeriod));
        }

        internal void Return(LibraryUser libraryUser)
        {
            if (InStock)
                throw new BookIsInStockException(Id);

            _inStock = true;

            AddDomainEvent(new BookReturnedEvent(Id, DateTime.UtcNow));
        }
    }
}
