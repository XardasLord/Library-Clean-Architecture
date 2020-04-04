using System.Collections.Generic;
using Library.Domain.Exceptions;
using Library.Domain.SeedWork;

namespace Library.Domain.AggregateModels.LoanAggregate
{
    public class BookInformation : ValueObject
    {
        public string Title { get; }
        public string Author { get; }
        public string Subject { get; }
        public string Isbn { get; }

        public BookInformation(string title, string author, string subject, string isbn)
        {
            if (string.IsNullOrWhiteSpace(title))
                throw new BookCreationException($"Parameter {nameof(title)} cannot be empty.");

            if (string.IsNullOrWhiteSpace(author))
                throw new BookCreationException($"Parameter {nameof(author)} cannot be empty.");

            if (string.IsNullOrWhiteSpace(subject))
                throw new BookCreationException($"Parameter {nameof(subject)} cannot be empty.");

            if (string.IsNullOrWhiteSpace(isbn))
                throw new BookCreationException($"Parameter {nameof(isbn)} cannot be empty.");


            Title = title;
            Author = author;
            Subject = subject;
            Isbn = isbn;
        }

        protected override IEnumerable<object> GetAtomicValues()
        {
            yield return Isbn;
        }
    }
}
