﻿using System;
using System.Collections.Generic;
using System.Linq;
using Library.Domain.AggregateModels.StorageAggregate.Events;
using Library.Domain.Exceptions;
using Library.Domain.SeedWork;

namespace Library.Domain.AggregateModels.StorageAggregate
{
    public class Storage : AggregateRoot<long>
    {
        private readonly List<Book> _books;
        public IReadOnlyCollection<Book> Books => _books;
        public IReadOnlyCollection<Book> AvailableBooks => _books.Where(x => x.InStock).ToList().AsReadOnly();

        private Storage() 
            => _books = new List<Book>();

        private Storage(long storageId) : base()
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

        public void BorrowBook(long bookId, long userId, DateTime fromDate, DateTime toDate)
        {
            if (fromDate < DateTime.UtcNow || toDate <= fromDate)
                throw new Exception("TODO Invalid dates range");

            var book = _books.SingleOrDefault(x => x.Id == bookId) 
                       ?? throw new BookNotFoundException(bookId);

            book.MarkAsUnavailable();

            AddDomainEvent(new BookBorrowedEvent(bookId, userId, fromDate, toDate));
        }

        public void ReturnBook(long bookId)
        {
            var book = _books.SingleOrDefault(x => x.Id == bookId)
                       ?? throw new BookNotFoundException(bookId);

            book.MarkAsAvailable();

            AddDomainEvent(new BookReturnedEvent(bookId, DateTime.UtcNow));
        }
    }
}