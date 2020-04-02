using System;
using System.Linq;
using FluentAssertions;
using Library.Domain.AggregateModels.BookAggregate;
using Library.Domain.AggregateModels.BookAggregate.Events;
using Library.Domain.Exceptions;
using Xunit;

namespace Library.Domain.Tests.Unit.AggregateModels.BookAggregate
{
    public class BorrowBookTests
    {
        internal void Act(Book book, long userId, int requestedLoadPeriodInDays) => book.Borrow(userId, requestedLoadPeriodInDays);

        [Fact]
        public void calling_borrow_book_for_valid_data_book_should_be_borrowed()
        {
            const long userId = 1;
            const int requestedLoadPeriodInDays = 15;
            var book = Book.Create("Title", "Author");

            Act(book, userId, requestedLoadPeriodInDays);

            book.IsBorrowed.Should().BeTrue();
            book.LoanUntil.Should().BeCloseTo(DateTime.UtcNow.AddDays(requestedLoadPeriodInDays));
            book.BorrowedByUserId.Should().Be(userId);
            book.DomainEvents.Last().Should().BeOfType<BookBorrowedEvent>();
        }

        [Theory]
        [InlineData(31)]
        [InlineData(0)]
        [InlineData(-1)]
        public void calling_borrow_book_with_invalid_requested_loan_period_should_throws_an_exception(int requestedLoadPeriodInDays)
        {
            const long userId = 1;
            var book = Book.Create("Title", "Author");

            var result = Record.Exception(() => Act(book, userId, requestedLoadPeriodInDays));

            result.Should().NotBeNull();
            result.Should().BeOfType<BookBorrowInvalidPeriodException>();
        }

        [Fact]
        public void calling_borrow_book_which_has_been_already_borrowed_should_throws_an_exception()
        {
            const long userId = 1;
            const int requestedLoadPeriodInDays = 15;
            var book = Book.Create("Title", "Author");

            book.Borrow(userId, requestedLoadPeriodInDays);

            var result = Record.Exception(() => Act(book, userId, requestedLoadPeriodInDays));

            result.Should().NotBeNull();
            result.Should().BeOfType<BookAlreadyBorrowedException>();
        }
    }
}
