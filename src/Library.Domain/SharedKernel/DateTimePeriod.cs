﻿using System;
using Library.Domain.AggregateModels.BookAggregate.Exceptions;

namespace Library.Domain.SharedKernel
{
    public record DateTimePeriod
    {
        public DateTime StartDate { get; }
        public DateTime EndDate { get; }

        private DateTimePeriod()
        {
        }

        private DateTimePeriod(DateTime startDate, DateTime endDate)
        {
            if (startDate >= endDate)
                throw new DateTimePeriodValidationException("Starting date cannot be greater or equal the ending date");

            StartDate = startDate;
            EndDate = endDate;
        }

        public static DateTimePeriod Create(DateTime startDate, DateTime endDate)
        {
            return new DateTimePeriod(startDate, endDate);
        }
    }
}
