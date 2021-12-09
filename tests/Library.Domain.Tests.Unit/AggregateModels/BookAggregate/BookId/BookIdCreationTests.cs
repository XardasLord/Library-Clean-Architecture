using System;
using FluentAssertions;
using Library.Tests.Base;
using Xunit;

namespace Library.Domain.Tests.Unit.AggregateModels.BookAggregate.BookId
{
    public class BookIdCreationTests : TestBase
    {
        private int _idValue;

        private Domain.AggregateModels.BookAggregate.BookId Act()
            => new(_idValue);
		
        [Fact]
        public void valid_Id_creates_bookId()
        {
            // Arrange
            _idValue = CreateInt();
			
            // Act
            var result = Act();
			
            // Assert
            result.Value.Should().Be(_idValue);
        }

        [Theory]
        [MemberData(nameof(IntNegativeAndZeroData))]
        public void negative_or_zero_Id_value_throws_exception(int invalidId)
        {
            // Assert
            _idValue = invalidId;
			
            // Act
            var result = Record.Exception(Act);
			
            // Assert
            result.Should().NotBeNull();
            result.Should().BeOfType<ArgumentException>();
        }
    }
}