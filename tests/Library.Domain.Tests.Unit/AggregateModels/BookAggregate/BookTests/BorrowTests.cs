using System;
using FluentAssertions;
using Library.Domain.AggregateModels.BookAggregate;
using Library.Domain.AggregateModels.BookAggregate.Exceptions;
using Library.Domain.AggregateModels.LibraryUserAggregate;
using Xunit;

namespace Library.Domain.Tests.Unit.AggregateModels.BookAggregate.BookTests
{
    public class BorrowTests
    {
        private Book _book;
        private readonly LibraryUser _libraryUser;

        public BorrowTests()
        {
            _libraryUser = LibraryUser.Create(
                new UserCredential("Login", "Password"),
                new Name("First", "Last"), 
                "Email@email.com");
        }

        private void Act(DateTimePeriod period) => _book.Borrow(_libraryUser, period);

        [Fact]
        public void when_book_is_in_stock_book_should_be_out_of_stock()
        {
            // Arrange
            var dateTimePeriod = DateTimePeriod.Create(DateTime.Now, DateTime.Now.AddDays(7));
            _book = Book.Create("Title", "Author", "Subject", "9783161484100");

            // Act
            Act(dateTimePeriod);

            // Assert
            _book.InStock.Should().BeFalse();
        }

        [Fact]
        public void when_book_is_not_in_stock_should_throws_an_exception()
        {
            // Arrange
            var dateTimePeriod = DateTimePeriod.Create(DateTime.Now, DateTime.Now.AddDays(7));
            _book = Book.Create("Title", "Author", "Subject", "9783161484100");
            _book.Borrow(_libraryUser, dateTimePeriod);

            // Act
            var result = Record.Exception(() => Act(dateTimePeriod));

            // Assert
            result.Should().NotBeNull();
            result.Should().BeOfType<BookIsNotInStockException>();
        }
    }
}
