using System.Linq;
using FluentAssertions;
using Library.Domain.AggregateModels.BookAggregate;
using Library.Domain.AggregateModels.BookAggregate.Events;
using Library.Domain.Exceptions;
using Xunit;

namespace Library.Domain.Tests.Unit.AggregateModels.BookAggregate
{
    public class GiveBackBookTests
    {
        internal void Act(Book book) => book.GiveBack();

        [Fact]
        public void calling_give_back_book_when_book_is_borrowed_should_be_set_as_available()
        {
            const long userId = 1;
            const uint daysPeriod = 15;
            var book = Book.Create("Title", "Author");

            book.Borrow(userId, daysPeriod);

            Act(book);

            book.IsBorrowed.Should().BeFalse();
            book.BorrowedUntil.Should().BeNull();
            book.BorrowedByUserId.Should().BeNull();
            book.DomainEvents.Last().Should().BeOfType<BookGaveBackEvent>();
        }

        [Fact]
        public void calling_give_back_book_when_book_is_not_borrowed_should_throws_an_exception()
        {
            var book = Book.Create("Title", "Author");

            var result = Record.Exception(() => Act(book));

            result.Should().NotBeNull();
            result.Should().BeOfType<BookNotBorrowedException>();
        }
    }
}
