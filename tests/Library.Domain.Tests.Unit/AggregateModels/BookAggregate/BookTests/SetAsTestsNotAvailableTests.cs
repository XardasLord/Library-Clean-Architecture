﻿using FluentAssertions;
using Library.Domain.AggregateModels.BookAggregate;
using Library.Domain.AggregateModels.BookAggregate.Exceptions;
using Library.Domain.AggregateModels.LibraryUserAggregate;
using Library.Domain.SharedKernel;
using Library.Domain.Tests.Unit.Helpers;
using Xunit;

namespace Library.Domain.Tests.Unit.AggregateModels.BookAggregate.BookTests
{
    public class SetAsTestsNotAvailableTests : AggregateTestHelper
    {
        private readonly Book _book;
        private readonly LibraryUser _libraryUser;
        private readonly DateTimePeriod _dateTimePeriod;

        public SetAsTestsNotAvailableTests()
        {
            _libraryUser = GetValidLibraryUserAggregate();
            _book = GetValidBookAggregate();
            _dateTimePeriod = GetValidDateTimePeriod();
        }

        private void Act()
            => _book.SetAsNotAvailable(_libraryUser.Id, _dateTimePeriod);

        [Fact]
        public void when_book_is_not_borrowed_should_be_out_of_stock()
        {
            // Arrange
            _book._inStock = true;

            // Act
            Act();

            // Assert
            _book.InStock.Should().BeFalse();
        }

        [Fact]
        public void when_book_is_already_borrowed_should_throws_an_exception()
        {
            // Arrange
            _book._inStock = false;

            // Act
            var result = Record.Exception(Act);

            // Assert
            result.Should().NotBeNull();
            result.Should().BeOfType<BookIsNotInStockException>();
        }
    }
}
