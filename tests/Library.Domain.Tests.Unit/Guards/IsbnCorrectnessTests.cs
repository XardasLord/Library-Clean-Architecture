using Ardalis.GuardClauses;
using FluentAssertions;
using Library.Domain.AggregateModels.BookAggregate.Exceptions;
using Library.Domain.AggregateModels.BookAggregate.Guards;
using Xunit;

namespace Library.Domain.Tests.Unit.Guards
{
    public class IsbnCorrectnessTests
    {
        private string _isbn;

        private string Act()
            => Guard.Against.IsbnCorrectness(_isbn, nameof(_isbn));
        
        [Theory]
        [InlineData("1112223334")] // 10 length
        [InlineData("9783161484100")] // 13 length
        public void given_valid_isbn_format_guard_should_pass(string validIsbn)
        {
            // Arrange
            _isbn = validIsbn;
            
            // Act
            var result = Act();

            // Assert
            result.Should().Be(validIsbn);
        }
        
        [Theory]
        [InlineData("111222333")] // 9 length
        [InlineData("11122233344")] // 11 length
        [InlineData("111222333444")] // 12 length
        [InlineData("11122233344455")] // 14 length
        public void given_invalid_isbn_format_guard_should_throw_an_exception(string invalidIsbn)
        {
            // Arrange
            _isbn = invalidIsbn;
            
            // Act
            var result = Record.Exception(Act);

            // Assert
            result.Should().NotBeNull();
            result.Should().BeOfType<BookIsbnInvalidFormatException>();
        }
    }
}