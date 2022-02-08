using FluentAssertions;
using Library.Domain.AggregateModels.LibraryUserAggregate;
using Library.Domain.AggregateModels.LibraryUserAggregate.Exceptions;
using Xunit;

namespace Library.Domain.Tests.Unit.AggregateModels.LibraryUserAggregate.LibraryUserTests
{
    public class CreateLibraryUser
    {
        private static LibraryUser Act(UserCredential credentials, Name name, string email)
            => LibraryUser.Create(credentials, name, email);

        [Fact]
        public void given_valid_data_library_user_should_be_created()
        {
            const string login = "Login";
            const string password = "Password";
            const string firstName = "FirstName";
            const string lastName = "LastName";
            const string emailAddress = "email@com.pl";
            var credentials = new UserCredential(login, password);
            var name = new Name(firstName, lastName);
            var email = new Email(emailAddress);

            var libraryUser = Act(credentials, name, email);

            libraryUser.Should().NotBeNull();
            libraryUser.Credentials.Login.Should().Be(login);
            libraryUser.Credentials.Password.Should().Be(password);
            libraryUser.FirstName.Should().Be(firstName);
            libraryUser.LastName.Should().Be(lastName);
            libraryUser.Email.Value.Should().Be(email);
            libraryUser.IsActive.Should().BeTrue();
        }

        // TODO: Move below tests as value object tests (Guards)
        // [Theory]
        // [InlineData(" ")]
        // [InlineData("")]
        // [InlineData(null)]
        // public void given_empty_login_should_throws_an_exception(string login)
        // {
        //     const string password = "Password";
        //     const string firstName = "FirstName";
        //     const string lastName = "LastName";
        //     const string emailAddress = "email@com.pl";
        //     var credentials = new UserCredential(login, password);
        //     var name = new Name(firstName, lastName);
        //     var email = new Email(emailAddress);
        //
        //     var result = Record.Exception(() => Act(credentials, name, email));
        //
        //     result.Should().NotBeNull();
        //     result.Should().BeOfType<LibraryUserCreationException>();
        // }

        //[Theory]
        //[InlineData(" ")]
        //[InlineData("")]
        //[InlineData(null)]
        //public void given_empty_password_should_throws_an_exception(string password)
        //{
        //    const string login = "Login";
        //    const string firstName = "FirstName";
        //    const string lastName = "LastName";
        //    const string email = "email@com.pl";
        //    var credentials = new UserCredential(login, password);

        //    var result = Record.Exception(() => Act(credentials, firstName, lastName, email));

        //    result.Should().NotBeNull();
        //    result.Should().BeOfType<LibraryUserCreationException>();
        //}

        //[Theory]
        //[InlineData("P")]
        //[InlineData("Pa")]
        //[InlineData("Pas")]
        //[InlineData("Pass")]
        //[InlineData("Pass1")]
        //public void given_too_short_password_should_throws_an_exception(string password)
        //{
        //    const string login = "Login";
        //    const string firstName = "FirstName";
        //    const string lastName = "LastName";
        //    const string email = "email@com.pl";
        //    var credentials = new UserCredential(login, password);

        //    var result = Record.Exception(() => Act(credentials, firstName, lastName, email));

        //    result.Should().NotBeNull();
        //    result.Should().BeOfType<LibraryUserCreationException>();
        //}

        //[Theory]
        //[InlineData(" ")]
        //[InlineData("")]
        //[InlineData(null)]
        //public void given_empty_firstname_should_throws_an_exception(string firstName)
        //{
        //    const string login = "Login";
        //    const string password = "Password";
        //    const string lastName = "LastName";
        //    const string email = "email@com.pl";
        //    var credentials = new UserCredential(login, password);
        //    var name = new Name(firstName, lastName);

        //    var result = Record.Exception(() => Act(credentials, name, email));

        //    result.Should().NotBeNull();
        //    result.Should().BeOfType<LibraryUserCreationException>();
        //}

        //[Theory]
        //[InlineData(" ")]
        //[InlineData("")]
        //[InlineData(null)]
        //public void given_empty_lastname_should_throws_an_exception(string lastName)
        //{
        //    const string login = "Login";
        //    const string password = "Password";
        //    const string firstName = "FirstName";
        //    const string email = "email@com.pl";
        //    var credentials = new UserCredential(login, password);

        //    var result = Record.Exception(() => Act(credentials, firstName, lastName, email));

        //    result.Should().NotBeNull();
        //    result.Should().BeOfType<LibraryUserCreationException>();
        //}

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
            var credentials = new UserCredential(login, password);
            var name = new Name(firstName, lastName);

            var result = Record.Exception(() => Act(credentials, name, email));

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
            const string login = "Login";
            const string password = "Password";
            const string firstName = "FirstName";
            const string lastName = "LastName";
            var credentials = new UserCredential(login, password);
            var name = new Name(firstName, lastName);

            var result = Record.Exception(() => Act(credentials, name, email));

            result.Should().NotBeNull();
            result.Should().BeOfType<InvalidEmailException>();
        }
    }
}
