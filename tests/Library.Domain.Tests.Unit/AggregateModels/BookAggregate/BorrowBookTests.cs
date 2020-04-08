using System.Linq;
using FluentAssertions;
using Library.Domain.AggregateModels.BookAggregate;
using Library.Domain.AggregateModels.BookAggregate.Events;
using Library.Domain.Exceptions;
using Xunit;

namespace Library.Domain.Tests.Unit.AggregateModels.BookAggregate
{
    public class BorrowBookTests
    {
        private Book _bookSut;

        private void Act() => _bookSut.Borrow();

        [Fact]
        public void when_book_is_in_stock_book_should_be_borrowed()
        {
            _bookSut = Book.Create("Title", "Author", "Subject", "9783161484100");

            Act();

            _bookSut.InStock.Should().BeFalse();
            _bookSut.DomainEvents.Last().Should().BeOfType<BookBorrowedEvent>();
        }

        [Fact]
        public void when_book_is_not_in_stock_should_throws_an_exception()
        {
            _bookSut = Book.Create("Title", "Author", "Subject", "9783161484100");
            _bookSut.Borrow();

            var result = Record.Exception(Act);

            result.Should().NotBeNull();
            result.Should().BeOfType<BookAlreadyBorrowedException>();
        }
    }
}
