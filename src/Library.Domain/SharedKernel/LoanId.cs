using Ardalis.GuardClauses;

namespace Library.Domain.SharedKernel
{
    public record LoanId
    {
        public long Value { get; }

        public LoanId(long value)
        {
            Value = Guard.Against.NegativeOrZero(value, nameof(LoanId));
        }
        
        public static implicit operator long(LoanId id)
            => id.Value;
        
        public static implicit operator LoanId(long id)
            => new(id);
    }
}