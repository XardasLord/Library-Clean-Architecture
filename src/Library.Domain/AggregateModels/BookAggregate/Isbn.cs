using System.Text.RegularExpressions;
using Ardalis.GuardClauses;
using Library.Domain.AggregateModels.BookAggregate.Guards;

namespace Library.Domain.AggregateModels.BookAggregate
{
    public record Isbn
    {
        public string Value { get; }

        private Isbn()
        {
        }

        public Isbn(string isbn)
        {
            Guard.Against.NullOrWhiteSpace(isbn, nameof(isbn));
            
            const string nonDigitsPattern = "[^.0-9]";
            isbn = Regex.Replace(isbn, nonDigitsPattern, string.Empty);

            Value = Guard.Against.IsbnCorrectness(isbn, nameof(isbn));
        }

        public static implicit operator string(Isbn isbn) => isbn.Value;
        public static implicit operator Isbn(string value) => new(value);

        public override string ToString() => Value;
    }
}