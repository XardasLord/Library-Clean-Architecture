using FluentAssertions;
using Library.Domain.AggregateModels.LibraryUserAggregate;
using Library.Domain.AggregateModels.LibraryUserAggregate.Exceptions;
using Xunit;

namespace Library.Domain.Tests.Unit.AggregateModels.LibraryUserAggregate.CredentialTests
{
    public class CreateCredentialTests
    { 
        private static UserCredential Act(string login, string password)
            => new UserCredential(login, password);
        
        [Theory]
         [InlineData(" ")]
         [InlineData("")]
         [InlineData(null)]
         public void given_empty_login_should_throws_an_exception(string login)
         {
             const string password = "Password";
        
             var result = Record.Exception(() => Act(login, password));
        
             result.Should().NotBeNull();
             result.Should().BeOfType<InvalidCredentialsException>();
         }

        [Theory]
        [InlineData(" ")]
        [InlineData("")]
        [InlineData(null)]
        public void given_empty_password_should_throws_an_exception(string password)
        {
            const string login = "Login";

            var result = Record.Exception(() => Act(login, password));

            result.Should().NotBeNull();
            result.Should().BeOfType<InvalidCredentialsException>();
        }

        [Theory]
        [InlineData("P")]
        [InlineData("Pa")]
        [InlineData("Pas")]
        [InlineData("Pass")]
        [InlineData("Pass1")]
        public void given_too_short_password_should_throws_an_exception(string password)
        {
            const string login = "Login";

            var result = Record.Exception(() => Act(login, password));

            result.Should().NotBeNull();
            result.Should().BeOfType<InvalidCredentialsException>();
        }
    }
}