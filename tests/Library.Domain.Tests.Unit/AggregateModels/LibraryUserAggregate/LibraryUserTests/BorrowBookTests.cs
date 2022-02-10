using System.Collections.Generic;
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
    public class BorrowBookTests : AggregateTestHelper
    {
        private readonly LibraryUser _libraryUser;
        private readonly Book _book;
        private readonly DateTimePeriod _dateTimePeriod;

        public BorrowBookTests()
        {
            _libraryUser = GetValidLibraryUserAggregate();
            _book = GetValidBookAggregate();
            _dateTimePeriod = GetValidDateTimePeriod();
        }

        private void Act()
            => _libraryUser.BorrowBook(_book, _dateTimePeriod);

        [Fact]
        public void when_library_user_borrows_available_book_should_has_new_loan_registered()
        {
            // Act
            Act();

            // Assert
            _libraryUser.ActiveLoans.Count.Should().Be(1);
            _libraryUser.ActiveLoans.First().IsActive.Should().BeTrue();
            
            _libraryUser.DomainEvents.Count.Should().Be(1);
            var @event = _libraryUser.DomainEvents.First();
            @event.Should().BeOfType<LibraryUserBorrowedBookEvent>();
            (@event as LibraryUserBorrowedBookEvent)?.BookId.Should().Be(_book.Id);
            (@event as LibraryUserBorrowedBookEvent)?.LibraryUserId.Should().Be(_libraryUser.Id);
        }

        [Fact]
        public void when_library_user_has_already_exceeded_maximum_value_of_borrowed_books_should_throws_an_exception()
        {
            // Arrange
            _libraryUser._activeLoans.AddRange(new List<Loan>
            {
                GetSampleLoanEntity(),
                GetSampleLoanEntity(),
                GetSampleLoanEntity()
            });

            // Act
            var result = Record.Exception(Act);

            // Assert
            result.Should().NotBeNull();
            result.Should().BeOfType<LibraryUserMaximumBooksBorrowedExceededException>();
        }
    }
}