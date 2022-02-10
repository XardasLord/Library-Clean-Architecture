using Library.Domain.Exceptions;

namespace Library.Domain.AggregateModels.BookAggregate.Exceptions
{
    public class BookBorrowInvalidPeriodException : DomainException
    {
        public override string Code => "invalid_days_period_for_borrowing_book";
        public int DaysPeriod { get; }
        public int MaximumAvailableLoanPeriodInDays { get; }

        public BookBorrowInvalidPeriodException(int requestedLoanPeriodInDays, int maximumAvailableLoanPeriodInDays) 
            : base($"Cannot borrow book for {requestedLoanPeriodInDays} days. The maximum period that book can be borrowed is {maximumAvailableLoanPeriodInDays} days.")
        {
            DaysPeriod = requestedLoanPeriodInDays;
            MaximumAvailableLoanPeriodInDays = maximumAvailableLoanPeriodInDays;
        }
    }
}
