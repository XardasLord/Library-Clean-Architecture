using System.Linq;
using FluentAssertions;
using Library.Domain.AggregateModels.BookAggregate;
using Library.Domain.AggregateModels.BookAggregate.Events;
using Library.Domain.Exceptions;
using Xunit;

namespace Library.Domain.Tests.Unit.AggregateModels.BookAggregate
{
    public class CreateBookTests
    {
        internal Book Act(BookInformation bookInformation) => Book.Create(bookInformation);

        [Fact]
        public void given_valid_data_book_should_be_created()
        {
            const string title = "Book Title";
            const string author = "Book Author";
            const string subject = "Subject";
            const string isbn = "Isbn";
            var bookInformation = new BookInformation(title, author, subject, isbn);

            var result = Act(bookInformation);

            result.Should().NotBeNull();
            result.BookInformation.Title.Should().Be(title);
            result.BookInformation.Author.Should().Be(author);
            result.BookInformation.Subject.Should().Be(subject);
            result.BookInformation.Isbn.Should().Be(isbn);
            result.InStock.Should().BeTrue();
            result.LoanUntil.Should().BeNull();

            result.DomainEvents.Should().HaveCount(1);
            result.DomainEvents.First().Should().BeOfType<BookCreatedEvent>();
        }

        [Theory]
        [InlineData("")]
        [InlineData(" ")]
        [InlineData(null)]
        public void given_invalid_title_book_creation_should_throw_an_exception(string title)
        {
            const string author = "Book Author";
            const string subject = "Subject";
            const string isbn = "Isbn";

            var result = Record.Exception(() => Act(new BookInformation(title, author, subject, isbn)));

            result.Should().NotBeNull();
            result.Should().BeOfType<BookCreationException>();
        }

        [Theory]
        [InlineData("")]
        [InlineData(" ")]
        [InlineData(null)]
        public void given_invalid_author_book_creation_should_throw_an_exception(string author)
        {
            const string title = "Title";
            const string subject = "Subject";
            const string isbn = "Isbn";

            var result = Record.Exception(() => Act(new BookInformation(title, author, subject, isbn)));

            result.Should().NotBeNull();
            result.Should().BeOfType<BookCreationException>();
        }

        [Theory]
        [InlineData("")]
        [InlineData(" ")]
        [InlineData(null)]
        public void given_invalid_subject_book_creation_should_throw_an_exception(string subject)
        {
            const string title = "Title";
            const string author = "Author";
            const string isbn = "Isbn";

            var result = Record.Exception(() => Act(new BookInformation(title, author, subject, isbn)));

            result.Should().NotBeNull();
            result.Should().BeOfType<BookCreationException>();
        }

        [Theory]
        [InlineData("")]
        [InlineData(" ")]
        [InlineData(null)]
        public void given_invalid_isbn_book_creation_should_throw_an_exception(string isbn)
        {
            const string title = "Title";
            const string author = "Author";
            const string subject = "Subject";

            var result = Record.Exception(() => Act(new BookInformation(title, author, subject, isbn)));

            result.Should().NotBeNull();
            result.Should().BeOfType<BookCreationException>();
        }
    }
}
