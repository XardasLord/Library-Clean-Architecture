using System;
using Library.Domain.AggregateModels.BookAggregate.Events;
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
