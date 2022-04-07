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

        public static Book Register(string title, string author, string subject, string isbn, long userId)
        {
            var bookInformation = new BookInformation(title, author, subject, isbn);
            var book = new Book(bookInformation);
            
            book.AddDomainEvent(new NewBookRegisteredEvent(book.Id, DateTime.UtcNow));

            return book;
        }
        
        public void SetAsNotAvailable(long libraryUserId, DateTimePeriod borrowPeriod)
        {
            if (!InStock)
                throw new BookIsNotInStockException();
            
            _inStock = false;

            AddDomainEvent(new BookBorrowedEvent(Id, libraryUserId, borrowPeriod));
        }

        public void SetAsAvailable(long libraryUserId)
        {
            if (InStock)
                throw new BookIsInStockException(Id);

            _inStock = true;

            AddDomainEvent(new BookReturnedEvent(Id, DateTime.UtcNow));
        }
    }
}
