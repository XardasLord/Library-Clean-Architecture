using System.Linq;
using FluentAssertions;
using Library.Domain.AggregateModels.BookAggregate;
using Library.Domain.AggregateModels.LibraryUserAggregate;
using Library.Domain.AggregateModels.LibraryUserAggregate.Events;
using Library.Domain.AggregateModels.LibraryUserAggregate.Exceptions;
using Library.Domain.SharedKernel;
using Library.Domain.Tests.Unit.Helpers;
using Xunit;

namespace Library.Domain.Tests.Unit.AggregateModels.LibraryUserAggregate.LibraryUserTests
{
    public class ReturnBookTests : AggregateTestHelper
    {
        private readonly LibraryUser _libraryUser;
        private readonly Book _book;
        private readonly DateTimePeriod _dateTimePeriod;

        public ReturnBookTests()
        {
            _libraryUser = GetValidLibraryUserAggregate();
            _book = GetValidBookAggregate();
            _dateTimePeriod = GetValidDateTimePeriod();
        }

        private void Act()
            => _libraryUser.ReturnBook(_book.Id);

        [Fact]
        public void when_library_user_returns_previously_borrowed_book_should_finish_its_loan()
        {
            // Arrange
            _libraryUser.BorrowBook(_book.Id, _dateTimePeriod);
            
            // Act
            Act();

            // Assert
            _libraryUser.ActiveLoans.Count.Should().Be(0);
            _libraryUser.DomainEvents.Count.Should().BePositive();
            
            var @event = _libraryUser.DomainEvents.Last();
            @event.Should().BeOfType<LibraryUserReturnedBookEvent>();
            (@event as LibraryUserReturnedBookEvent)?.BookId.Should().Be(_book.Id);
            (@event as LibraryUserReturnedBookEvent)?.LibraryUserId.Should().Be(_libraryUser.Id);
        }

        [Fact]
        public void when_library_user_does_not_have_borrowed_given_book_should_throws_an_exception()
        {
            // Act
            var result = Record.Exception(Act);

            // Assert
            result.Should().NotBeNull();
            result.Should().BeOfType<LibraryUserDoesNotHaveBookBorrowed>();
        }
    }
}