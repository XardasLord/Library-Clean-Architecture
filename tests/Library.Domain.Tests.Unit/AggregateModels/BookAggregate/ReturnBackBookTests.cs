using System.Linq;
using FluentAssertions;
using Library.Domain.AggregateModels.BookAggregate;
using Library.Domain.AggregateModels.BookAggregate.Events;
using Library.Domain.Exceptions;
using Xunit;

namespace Library.Domain.Tests.Unit.AggregateModels.BookAggregate
{
    public class ReturnBackBookTests
    {
        internal void Act(Book book) => book.ReturnBack();

        [Fact]
        public void calling_give_back_book_when_book_is_borrowed_should_be_set_as_available()
        {
            const long userId = 1;
            const int requestedLoadPeriodInDays = 15;
            var bookInformation = new BookInformation("Title", "Author", "Subject", "isbn");
            var book = Book.Create(bookInformation);

            book.Borrow(userId, requestedLoadPeriodInDays);

            Act(book);

            book.InStock.Should().BeTrue();
            book.LoanUntil.Should().BeNull();
            book.BorrowedByUserId.Should().BeNull();
            book.DomainEvents.Last().Should().BeOfType<BookReturnedBackEvent>();
        }

        [Fact]
        public void calling_give_back_book_when_book_is_not_borrowed_should_throws_an_exception()
        {
            var bookInformation = new BookInformation("Title", "Author", "Subject", "isbn");
            var book = Book.Create(bookInformation);

            var result = Record.Exception(() => Act(book));

            result.Should().NotBeNull();
            result.Should().BeOfType<BookNotBorrowedException>();
        }
    }
}
