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
        internal void Act(Book book, long userId, uint daysPeriod) => book.Borrow(userId, daysPeriod);

        [Fact]
        public void calling_borrow_book_for_valid_data_book_should_be_borrowed()
        {
            const long userId = 1;
            const uint daysPeriod = 15;
            var book = Book.Create("Title", "Author");

            Act(book, userId, daysPeriod);

            book.IsBorrowed.Should().BeTrue();
            book.BorrowedUntil.Should().BeCloseTo(DateTime.UtcNow.AddDays(daysPeriod));
            book.BorrowedByUserId.Should().Be(userId);
            book.DomainEvents.Last().Should().BeOfType<BookBorrowedEvent>();
        }

        [Fact]
        public void calling_borrow_book_with_invalid_days_period_should_throws_an_exception()
        {
            const long userId = 1;
            const uint daysPeriod = 31;
            var book = Book.Create("Title", "Author");

            var result = Record.Exception(() => Act(book, userId, daysPeriod));

            result.Should().NotBeNull();
            result.Should().BeOfType<BookBorrowInvalidPeriodException>();
        }

        [Fact]
        public void calling_borrow_book_which_has_been_already_borrowed_should_throws_an_exception()
        {
            const long userId = 1;
            const uint daysPeriod = 15;
            var book = Book.Create("Title", "Author");

            book.Borrow(userId, daysPeriod);

            var result = Record.Exception(() => Act(book, userId, daysPeriod));

            result.Should().NotBeNull();
            result.Should().BeOfType<BookAlreadyBorrowedException>();
        }
    }
}
