using System;
using Library.Domain.AggregateModels.BookAggregate.Events;
using Library.Domain.Exceptions;
using Library.Domain.SeedWork;

namespace Library.Domain.AggregateModels.BookAggregate
{
    public class Book : Entity<long>, IAggregateRoot
    {
        public string Title { get; }
        public string Author { get; }
        public bool IsReserved { get; }
        public DateTime? ReservedUntil { get; }

        private Book(string title, string author)
        {
            if (string.IsNullOrWhiteSpace(title))
                throw new BookCreationException($"Parameter {nameof(title)} cannot be empty.");

            if (string.IsNullOrWhiteSpace(author))
                throw new BookCreationException($"Parameter {nameof(author)} cannot be empty.");

            Title = title;
            Author = author;
            IsReserved = false;
            ReservedUntil = null;
        }

        public static Book Create(string title, string author)
        {
            var book = new Book(title, author);

            book.AddDomainEvent(new BookCreatedEvent(title, author));

            return book;
        }
    }
}
