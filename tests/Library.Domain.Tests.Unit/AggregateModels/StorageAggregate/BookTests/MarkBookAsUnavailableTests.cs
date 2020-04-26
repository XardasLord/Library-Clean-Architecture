using FluentAssertions;
using Library.Domain.AggregateModels.StorageAggregate;
using Library.Domain.Exceptions;
using Xunit;

namespace Library.Domain.Tests.Unit.AggregateModels.StorageAggregate.BookTests
{
    public class MarkBookAsUnavailableTests
    {
        private Book _bookSut;

        private void Act() => _bookSut.MarkAsUnavailable();

        [Fact]
        public void when_book_is_in_stock_book_should_be_out_of_stock()
        {
            _bookSut = Book.Create("Title", "Author", "Subject", "9783161484100");

            Act();

            _bookSut.InStock.Should().BeFalse();
        }

        [Fact]
        public void when_book_is_not_in_stock_should_throws_an_exception()
        {
            _bookSut = Book.Create("Title", "Author", "Subject", "9783161484100");
            _bookSut.MarkAsUnavailable();

            var result = Record.Exception(Act);

            result.Should().NotBeNull();
            result.Should().BeOfType<BookIsNotInStockException>();
        }
    }
}
