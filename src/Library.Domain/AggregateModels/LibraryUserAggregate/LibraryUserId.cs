using Ardalis.GuardClauses;

namespace Library.Domain.AggregateModels.LibraryUserAggregate
{
    public record LibraryUserId
    {
        public long Value { get; }

        public LibraryUserId(long value)
        {
            Value = Guard.Against.NegativeOrZero(value, nameof(LibraryUserId));
        }
        
        public static implicit operator long(LibraryUserId id)
            => id.Value;
        
        public static implicit operator LibraryUserId(long id)
            => new(id);
    }
}