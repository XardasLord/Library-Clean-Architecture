using FluentAssertions;
using Library.Domain.AggregateModels.StorageAggregate;
using Library.Domain.Exceptions;
using Xunit;

namespace Library.Domain.Tests.Unit.AggregateModels.StorageAggregate
{
    public class MarkBookAsAvailableTests
    {
        private Book _bookSut;

        private void Act() => _bookSut.MarkAsAvailable();

        [Fact]
        public void when_book_is_borrowed_book_should_be_returned()
        {
            _bookSut = Book.Create("Title", "Author", "Subject", "9783161484100");
            _bookSut.MarkAsUnavailable();

            Act();

            _bookSut.InStock.Should().BeTrue();
        }

        [Fact]
        public void when_book_is_in_stock_should_throws_an_exception()
        {
            _bookSut = Book.Create("Title", "Author", "Subject", "9783161484100");

            var result = Record.Exception(Act);

            result.Should().NotBeNull();
            result.Should().BeOfType<BookIsInStockException>();
        }
    }
}
