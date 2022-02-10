using FluentAssertions;
using Library.Domain.AggregateModels.LibraryUserAggregate;
using Xunit;

namespace Library.Domain.Tests.Unit.AggregateModels.LibraryUserAggregate.EmailTests
{
    public class ToStringTests
    {
        private static Email Act(string email) => new(email);

        [Fact]
        public void calling_ToString_on_email_should_return_its_value()
        {
            // Arrange
            const string validEmail = "valid@email.com";
            
            // Act
            var result = Act(validEmail);

            // Assert
            result.Value.Should().Be(validEmail);
            result.ToString().Should().Be(validEmail);
        }
    }
}