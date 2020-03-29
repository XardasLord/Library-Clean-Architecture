using System;
using Library.Domain.SeedWork;

namespace Library.Domain.AggregateModels.BookAggregate
{
    public class Book : Entity<long>
    {
        public string Title { get; }
        public string Author { get; }
        public bool IsReserved { get; }
        public DateTime? ReservedUntil { get; }

        public Book(string title, string author)
        {
            Title = title;
            Author = author;
            IsReserved = false;
            ReservedUntil = null;
        }
    }
}
