using System;
using System.Collections.Generic;
using Library.Domain.Exceptions;
using Library.Domain.SeedWork;

namespace Library.Domain.AggregateModels.StorageAggregate
{
    public class DateTimePeriod : ValueObject
    {
        public DateTime StartDate { get; }
        public DateTime EndDate { get; }

        private DateTimePeriod() { }

        private DateTimePeriod(DateTime startDate, DateTime endDate)
        {
            if (startDate.Date < DateTime.UtcNow.Date)
                throw new DateTimePeriodValidationException("Starting date cannot be in the past.");

            if (startDate >= endDate)
                throw new DateTimePeriodValidationException("Starting date cannot be greater or equal the ending date");

            StartDate = startDate;
            EndDate = endDate;
        }

        public static DateTimePeriod Create(DateTime startDate, DateTime endDate)
        {
            return new DateTimePeriod(startDate, endDate);
        }

        protected override IEnumerable<object> GetAtomicValues()
        {
            yield return StartDate;
            yield return EndDate;
        }
    }
}
