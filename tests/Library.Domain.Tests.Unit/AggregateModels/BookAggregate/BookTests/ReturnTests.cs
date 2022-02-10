using FluentAssertions;
using Library.Domain.AggregateModels.BookAggregate;
using Library.Domain.AggregateModels.BookAggregate.Exceptions;
using Library.Domain.AggregateModels.LibraryUserAggregate;
using Library.Domain.Tests.Unit.Helpers;
using Xunit;

namespace Library.Domain.Tests.Unit.AggregateModels.BookAggregate.BookTests
{
    public class ReturnTests : AggregateTestHelper
    {
        private readonly Book _book;
        private readonly LibraryUser _libraryUser;

        public ReturnTests()
        {
            _libraryUser = GetValidLibraryUserAggregate();
            _book = GetValidBookAggregate();
        }
        
        private void Act() 
            => _book.Return(_libraryUser);

        [Fact]
        public void when_book_is_borrowed_book_should_be_returned()
        {
            // Arrange
            _book._inStock = false;

            // Act
            Act();

            // Assert
            _book.InStock.Should().BeTrue();
        }

        [Fact]
        public void when_book_is_not_borrowed_should_throws_an_exception()
        {
            // Arrange
            _book._inStock = true;

            // Act
            var result = Record.Exception(Act);

            // Assert
            result.Should().NotBeNull();
            result.Should().BeOfType<BookIsInStockException>();
        }
    }
}
