using System.Linq;
using FluentAssertions;
using Library.Domain.AggregateModels.BookAggregate;
using Library.Domain.AggregateModels.BookAggregate.Events;
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
            result.IsReserved.Should().BeFalse();
            result.ReservedUntil.Should().BeNull();

            result.DomainEvents.Should().HaveCount(1);
            result.DomainEvents.First().Should().BeOfType<BookCreatedEvent>();
        }
    }
}
