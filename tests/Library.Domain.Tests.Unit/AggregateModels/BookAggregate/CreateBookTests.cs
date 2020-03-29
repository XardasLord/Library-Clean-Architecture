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
        internal Book Act(string title, string author) => Book.Create(title, author);

        [Fact]
        public void given_valid_data_book_should_be_created()
        {
            const string title = "Book Title";
            const string author = "Book Author";

            var result = Act(title, author);

            result.Should().NotBeNull();
            result.Title.Should().Be(title);
            result.Author.Should().Be(author);
            result.IsBorrowed.Should().BeFalse();
            result.BorrowedUntil.Should().BeNull();

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

            var result = Record.Exception(() => Act(title, author));

            result.Should().NotBeNull();
            result.Should().BeOfType<BookCreationException>();
        }
    }
}
