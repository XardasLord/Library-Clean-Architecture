using FluentAssertions;
using Library.Domain.AggregateModels.LibraryUserAggregate;
using Library.Domain.Tests.Unit.Helpers;
using Xunit;

namespace Library.Domain.Tests.Unit.AggregateModels.LibraryUserAggregate.LibraryUserTests
{
    public class CreateLibraryUser : AggregateTestHelper
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
    }
}
