using Ardalis.GuardClauses;

namespace Library.Domain.AggregateModels.BookAggregate
{
    public record BookInformation
    {
        public string Title { get; init; }
        public string Author { get; init;  }
        public string Subject { get; init;  }
        public Isbn Isbn { get; init;  }

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
    }
}
