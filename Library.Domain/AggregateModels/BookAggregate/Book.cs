using System;
using Library.Domain.AggregateModels.BookAggregate.Events;
using Library.Domain.Exceptions;
using Library.Domain.SeedWork;

namespace Library.Domain.AggregateModels.BookAggregate
{
    public class Book : AggregateRoot<long>
    {
        private BookInformation _bookInformation;
        private bool _inStock;

        public BookInformation BookInformation => _bookInformation;
        public bool InStock => _inStock;

        private Book(BookInformation bookInformation)
        {
            _bookInformation = bookInformation;
            _inStock = true;
        }

        public static Book Create(string title, string author, string subject, string isbn)
        {
            var bookInformation = new BookInformation(title, author, subject, isbn);
            var book = new Book(bookInformation);

            book.AddDomainEvent(new BookCreatedEvent(bookInformation));

            return book;
        }

        public void Borrow()
        {
            if (!InStock)
                throw new BookAlreadyBorrowedException();

            _inStock = false;

            AddDomainEvent(new BookBorrowedEvent(Id));
        }

        public void Return()
        {
            if (InStock)
                throw new BookNotBorrowedException(Id);

            _inStock = true;

            AddDomainEvent(new BookReturnedBackEvent(Id, DateTime.UtcNow));
        }
    }
}
