using System;
using Library.Domain.AggregateModels.BookAggregate.Events;
using Library.Domain.AggregateModels.BookAggregate.Exceptions;
using Library.Domain.AggregateModels.LibraryUserAggregate;
using Library.Domain.SeedWork;

namespace Library.Domain.AggregateModels.BookAggregate
{
    public class Book : Entity<long>, IAggregateRoot
    {
        private BookInformation _bookInformation;
        private bool _inStock;

        public BookInformation BookInformation => _bookInformation;
        public bool InStock => _inStock;

        private Book() { }

        private Book(BookInformation bookInformation)
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
        
        public void Borrow(LibraryUser libraryUser, DateTimePeriod dateTimePeriod)
        {
            if (!InStock)
                throw new BookIsNotInStockException();
            
            _inStock = false;

            AddDomainEvent(new BookBorrowedEvent(Id, libraryUser.Id, dateTimePeriod));
        }

        public void Return(LibraryUser libraryUser)
        {
            if (InStock)
                throw new BookIsInStockException(Id);

            _inStock = true;
            
            AddDomainEvent(new BookReturnedEvent(Id, DateTime.UtcNow));
        }
    }
}
