using FluentAssertions;
using Library.Domain.AggregateModels.LibraryUserAggregate;
using Library.Domain.AggregateModels.LibraryUserAggregate.Exceptions;
using Xunit;

namespace Library.Domain.Tests.Unit.AggregateModels.LibraryUserAggregate.EmailTests
{
    public class CreateEmailTests
    {
        private static Email Act(string email) => new(email);

        [Theory]
        [InlineData(" ")]
        [InlineData("")]
        [InlineData(null)]
        public void given_empty_email_should_throws_an_exception(string email)
        {
            // Act
            var result = Record.Exception(() => Act(email));

            // Assert
            result.Should().NotBeNull();
            result.Should().BeOfType<InvalidEmailException>();
        }

        [Theory]
        [InlineData("Email")]
        [InlineData("Email@")]
        [InlineData("@Email")]
        [InlineData("@Email@com")]
        [InlineData("Email@com@com")]
        public void given_invalid_email_format_should_throws_an_exception(string email)
        {
            // Act
            var result = Record.Exception(() => Act(email));

            // Assert
            result.Should().NotBeNull();
            result.Should().BeOfType<InvalidEmailException>();
        }
    }
}