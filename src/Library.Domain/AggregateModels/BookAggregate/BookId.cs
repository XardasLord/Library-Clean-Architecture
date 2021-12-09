using Ardalis.GuardClauses;

namespace Library.Domain.AggregateModels.BookAggregate
{
    public record BookId
    {
        public long Value { get; }

        public BookId(long value)
        {
            Value = Guard.Against.NegativeOrZero(value, nameof(BookId));
        }
        
        public static implicit operator long(BookId id)
            => id.Value;
        
        public static implicit operator BookId(long id)
            => new(id);
    }
}