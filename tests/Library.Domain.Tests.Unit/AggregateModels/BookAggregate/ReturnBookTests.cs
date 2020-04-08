using System.Linq;
using FluentAssertions;
using Library.Domain.AggregateModels.BookAggregate;
using Library.Domain.AggregateModels.BookAggregate.Events;
using Library.Domain.Exceptions;
using Xunit;

namespace Library.Domain.Tests.Unit.AggregateModels.BookAggregate
{
    public class ReturnBookTests
    {
        private Book _bookSut;

        private void Act() => _bookSut.Return();

        [Fact]
        public void when_book_is_borrowed_book_should_be_returned()
        {
            _bookSut = Book.Create("Title", "Author", "Subject", "9783161484100");
            _bookSut.Borrow();

            Act();

            _bookSut.InStock.Should().BeTrue();
            _bookSut.DomainEvents.Last().Should().BeOfType<BookReturnedBackEvent>();
        }

        [Fact]
        public void when_book_is_in_stock_should_throws_an_exception()
        {
            _bookSut = Book.Create("Title", "Author", "Subject", "9783161484100");

            var result = Record.Exception(Act);

            result.Should().NotBeNull();
            result.Should().BeOfType<BookNotBorrowedException>();
        }
    }
}
