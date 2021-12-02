using System.Linq;
using FluentAssertions;
using Library.Domain.AggregateModels.BookAggregate;
using Library.Domain.AggregateModels.BookAggregate.Events;
using Library.Domain.AggregateModels.LibraryUserAggregate;
using Xunit;

namespace Library.Domain.Tests.Unit.AggregateModels.BookAggregate.BookTests
{
    public class RegisterTests
    {
        private Book _book;
        private readonly LibraryUser _libraryUser;

        public RegisterTests()
        {
            _libraryUser = LibraryUser.Create(
                new UserCredential("Login", "Password"),
                new Name("First", "Last"), 
                "Email@email.com");
        }

        private void Act() => _book.Register(_libraryUser);

        [Fact]
        public void given_new_book_should_be_added()
        {
            // Arrange
            const string title = "Book Title";
            const string author = "Book Author";
            const string subject = "Subject";
            const string isbn = "9783161484100";
            _book = Book.Create(title, author, subject, isbn);

            // Act
            Act();

            // Assert
            _book.InStock.Should().BeTrue();
            _book.DomainEvents.Single().Should().BeOfType<NewBookRegisteredEvent>();
        }
    }
}
