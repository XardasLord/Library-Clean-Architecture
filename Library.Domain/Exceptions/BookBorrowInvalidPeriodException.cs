namespace Library.Domain.Exceptions
{
    public class BookBorrowInvalidPeriodException : DomainException
    {
        protected override string Code => "invalid_days_period_for_borrowing_book";
        public uint DaysPeriod { get; }

        public BookBorrowInvalidPeriodException(uint daysPeriod) 
            : base($"Cannot borrow book for {daysPeriod} days. The maximum period that book can be borrowed is 30 days.") 
            => DaysPeriod = daysPeriod;
    }
}
