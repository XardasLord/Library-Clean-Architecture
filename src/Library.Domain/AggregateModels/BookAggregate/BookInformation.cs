using System.Collections.Generic;
using Ardalis.GuardClauses;
using Library.Domain.SeedWork;

namespace Library.Domain.AggregateModels.BookAggregate
{
    public class BookInformation : ValueObject
    {
        public string Title { get; }
        public string Author { get; }
        public string Subject { get; }
        public Isbn Isbn { get; }

        private BookInformation()
        {
        }

        public BookInformation(string title, string author, string subject, string isbn)
        {
            Title = Guard.Against.NullOrWhiteSpace(title, nameof(title));
            Author = Guard.Against.NullOrWhiteSpace(author, nameof(author));
            Subject = Guard.Against.NullOrWhiteSpace(subject, nameof(subject));
            Isbn = new Isbn(isbn);
        }

        protected override IEnumerable<object> GetAtomicValues()
        {
            yield return Isbn;
        }
    }
}
