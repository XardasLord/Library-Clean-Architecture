using FluentAssertions;
using Library.Domain.AggregateModels.LibraryUserAggregate;
using Library.Domain.Exceptions;
using Xunit;

namespace Library.Domain.Tests.Unit.AggregateModels.LibraryUserAggregate
{
    public class CreateLibraryUser
    {
        private static LibraryUser Act(string login, string password, string firstName, string lastName, string email)
            => LibraryUser.Create(login, password, firstName, lastName, email);

        [Fact]
        public void given_valid_data_library_user_should_be_created()
        {
            const string login = "Login";
            const string password = "Password";
            const string firstName = "FirstName";
            const string lastName = "LastName";
            const string email = "email@com.pl";

            var libraryUser = Act(login, password, firstName, lastName, email);

            libraryUser.Should().NotBeNull();
            libraryUser.Login.Should().Be(login);
            libraryUser.Password.Should().Be(password);
            libraryUser.FirstName.Should().Be(firstName);
            libraryUser.LastName.Should().Be(lastName);
            libraryUser.Email.Value.Should().Be(email);
            libraryUser.Active.Should().BeTrue();
        }

        [Theory]
        [InlineData(" ")]
        [InlineData("")]
        [InlineData(null)]
        public void given_empty_login_should_throws_an_exception(string login)
        {
            const string password = "Password";
            const string firstName = "FirstName";
            const string lastName = "LastName";
            const string email = "email@com.pl";

            var result = Record.Exception(() => Act(login, password, firstName, lastName, email));

            result.Should().NotBeNull();
            result.Should().BeOfType<LibraryUserCreationException>();
        }

        [Theory]
        [InlineData(" ")]
        [InlineData("")]
        [InlineData(null)]
        public void given_empty_password_should_throws_an_exception(string password)
        {
            const string login = "Login";
            const string firstName = "FirstName";
            const string lastName = "LastName";
            const string email = "email@com.pl";

            var result = Record.Exception(() => Act(login, password, firstName, lastName, email));

            result.Should().NotBeNull();
            result.Should().BeOfType<LibraryUserCreationException>();
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
            const string firstName = "FirstName";
            const string lastName = "LastName";
            const string email = "email@com.pl";

            var result = Record.Exception(() => Act(login, password, firstName, lastName, email));

            result.Should().NotBeNull();
            result.Should().BeOfType<LibraryUserCreationException>();
        }

        [Theory]
        [InlineData(" ")]
        [InlineData("")]
        [InlineData(null)]
        public void given_empty_firstname_should_throws_an_exception(string firstName)
        {
            const string login = "Login";
            const string password = "Password";
            const string lastName = "LastName";
            const string email = "email@com.pl";

            var result = Record.Exception(() => Act(login, password, firstName, lastName, email));

            result.Should().NotBeNull();
            result.Should().BeOfType<LibraryUserCreationException>();
        }

        [Theory]
        [InlineData(" ")]
        [InlineData("")]
        [InlineData(null)]
        public void given_empty_lastname_should_throws_an_exception(string lastName)
        {
            const string login = "Login";
            const string password = "Password";
            const string firstName = "FirstName";
            const string email = "email@com.pl";

            var result = Record.Exception(() => Act(login, password, firstName, lastName, email));

            result.Should().NotBeNull();
            result.Should().BeOfType<LibraryUserCreationException>();
        }

        [Theory]
        [InlineData(" ")]
        [InlineData("")]
        [InlineData(null)]
        public void given_empty_email_should_throws_an_exception(string email)
        {
            const string login = "Login";
            const string password = "Password";
            const string firstName = "FirstName";
            const string lastName = "LastName";

            var result = Record.Exception(() => Act(login, password, firstName, lastName, email));

            result.Should().NotBeNull();
            result.Should().BeOfType<LibraryUserCreationException>();
        }

        [Theory]
        [InlineData("Email")]
        [InlineData("Email@")]
        [InlineData("@Email")]
        [InlineData("@Email@com")]
        [InlineData("Email@com@com")]
        public void given_invalid_email_format_should_throws_an_exception(string email)
        {
            const string login = "Login";
            const string password = "Password";
            const string firstName = "FirstName";
            const string lastName = "LastName";

            var result = Record.Exception(() => Act(login, password, firstName, lastName, email));

            result.Should().NotBeNull();
            result.Should().BeOfType<InvalidEmailException>();
        }
    }
}
