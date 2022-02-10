using Library.Domain.Exceptions;

namespace Library.Domain.AggregateModels.BookAggregate.Exceptions
{
    public class DateTimePeriodValidationException : DomainException
    {
        public override string Code => "Datetime period validation failed.";

        public DateTimePeriodValidationException(string message) : base(message)
        {
        }
    }
}
