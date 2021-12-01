using System;
using System.Collections.Generic;
using System.Linq;
using Library.Domain.AggregateModels.StorageAggregate.Events;
using Library.Domain.Exceptions;
using Library.Domain.SeedWork;

namespace Library.Domain.AggregateModels.StorageAggregate
{
    public class Storage : Entity<long>, IAggregateRoot
    {
        private readonly List<Book> _books;
        private readonly List<Loan> _loans;
        public IReadOnlyCollection<Book> Books => _books;
        public IReadOnlyCollection<Loan> Loans => _loans;
        public IReadOnlyCollection<Book> AvailableBooks => _books.Where(x => x.InStock).ToList().AsReadOnly();

        private Storage()
        {
            _books = new List<Book>();
            _loans = new List<Loan>();
        }

        private Storage(long storageId) : this()
            => Id = storageId;

        public static Storage Create(long id)
            => new Storage(id);

        public void AddBook(Book newBook)
        {
            if (_books.Any(x => x.Id == newBook.Id && newBook.Id > 0))
                throw new BookAlreadyExistsInStorageException(newBook.Id);

            _books.Add(newBook);

            AddDomainEvent(new BookAddedToStorageEvent(newBook, DateTime.UtcNow));
        }

        public void BorrowBook(long bookId, long userId, DateTimePeriod dateTimePeriod)
        {
            var book = _books.SingleOrDefault(x => x.Id == bookId)
                       ?? throw new BookNotFoundException(bookId);

            if (!book.InStock)
                throw new BookIsNotInStockException();

            _loans.Add(Loan.Create(bookId, userId, dateTimePeriod));

            book.MarkAsUnavailable();

            AddDomainEvent(new BookBorrowedEvent(bookId, userId, dateTimePeriod));
        }

        public void ReturnBook(long bookId, long userId)
        {
            var book = _books.SingleOrDefault(x => x.Id == bookId)
                       ?? throw new BookNotFoundException(bookId);

            var loan = _loans.SingleOrDefault(x => x.BookId == bookId && x.UserId == userId)
                       ?? throw new BookNotBorrowedForUserException(bookId);

            book.MarkAsAvailable();
            loan.EndLoan();

            AddDomainEvent(new BookReturnedEvent(bookId, DateTime.UtcNow));
        }
    }
}
