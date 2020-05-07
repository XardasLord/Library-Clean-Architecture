using System;
using FluentAssertions;
using Library.Domain.AggregateModels.StorageAggregate;
using Library.Domain.Exceptions;
using Xunit;

namespace Library.Domain.Tests.Unit.AggregateModels.StorageAggregate.DateTimePeriodTests
{
    public class CreateDateTimePeriodTests
    {
        internal DateTimePeriod Act(DateTime startDate, DateTime endDate) 
            => DateTimePeriod.Create(startDate, endDate);

        [Fact]
        public void given_valid_dates_should_be_created()
        {
            var startDate = DateTime.UtcNow.AddDays(1);
            var endDate = DateTime.UtcNow.AddMonths(1);

            var result = Act(startDate, endDate);

            result.StartDate.Should().BeCloseTo(startDate);
            result.EndDate.Should().BeCloseTo(endDate);
        }

        [Fact]
        public void given_start_date_in_the_past_should_throws_an_exception()
        {
            var startDate = DateTime.UtcNow.AddDays(-1);
            var endDate = DateTime.UtcNow.AddMonths(1);

            var result = Record.Exception(() => Act(startDate, endDate));

            result.Should().NotBeNull();
            result.Should().BeOfType<DateTimePeriodValidationException>();
        }

        [Fact]
        public void given_start_date_the_same_as_end_date_should_throws_an_exception()
        {
            var startDate = DateTime.UtcNow.AddDays(1);
            var endDate = startDate;

            var result = Record.Exception(() => Act(startDate, endDate));

            result.Should().NotBeNull();
            result.Should().BeOfType<DateTimePeriodValidationException>();
        }

        [Fact]
        public void given_start_date_greater_than_end_date_should_throws_an_exception()
        {
            var startDate = DateTime.UtcNow.AddDays(2);
            var endDate = DateTime.UtcNow.AddDays(1);

            var result = Record.Exception(() => Act(startDate, endDate));

            result.Should().NotBeNull();
            result.Should().BeOfType<DateTimePeriodValidationException>();
        }
    }
}
