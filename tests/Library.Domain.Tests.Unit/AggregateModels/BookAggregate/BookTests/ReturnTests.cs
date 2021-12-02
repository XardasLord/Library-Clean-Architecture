using System;
using FluentAssertions;
using Library.Domain.AggregateModels.BookAggregate;
using Library.Domain.AggregateModels.BookAggregate.Exceptions;
using Library.Domain.AggregateModels.LibraryUserAggregate;
using Xunit;

namespace Library.Domain.Tests.Unit.AggregateModels.BookAggregate.BookTests
{
    public class ReturnTests
    {
        private Book _book;
        private readonly LibraryUser _libraryUser;

        public ReturnTests()
        {
            _libraryUser = LibraryUser.Create(
                new UserCredential("Login", "Password"),
                new Name("First", "Last"), 
                "Email@email.com");
        }
        
        private void Act() => _book.Return(_libraryUser);

        [Fact]
        public void when_book_is_borrowed_book_should_be_returned()
        {
            // Arrange
            var dateTimePeriod = DateTimePeriod.Create(DateTime.Now, DateTime.Now.AddDays(7));
            _book = Book.Create("Title", "Author", "Subject", "9783161484100");
            _book.Borrow(_libraryUser, dateTimePeriod);

            // Act
            Act();

            // Assert
            _book.InStock.Should().BeTrue();
        }

        [Fact]
        public void when_book_is_in_stock_should_throws_an_exception()
        {
            _book = Book.Create("Title", "Author", "Subject", "9783161484100");

            var result = Record.Exception(Act);

            result.Should().NotBeNull();
            result.Should().BeOfType<BookIsInStockException>();
        }
    }
}
