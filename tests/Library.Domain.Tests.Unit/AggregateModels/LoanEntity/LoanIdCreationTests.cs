using System;
using FluentAssertions;
using Library.Domain.SharedKernel;
using Library.Tests.Base;
using Xunit;

namespace Library.Domain.Tests.Unit.AggregateModels.LoanEntity
{
    public class LoanIdCreationTests : TestBase
    {
        private int _idValue;

        private LoanId Act()
            => new(_idValue);
		
        [Fact]
        public void valid_Id_creates_LoanId()
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