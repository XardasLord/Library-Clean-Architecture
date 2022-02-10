using System.Linq;
using FluentAssertions;
using Library.Domain.AggregateModels.BookAggregate;
using Library.Domain.AggregateModels.BookAggregate.Events;
using Library.Domain.AggregateModels.LibraryUserAggregate;
using Library.Domain.Tests.Unit.Helpers;
using NSubstitute.Core;
using Xunit;

namespace Library.Domain.Tests.Unit.AggregateModels.BookAggregate.BookTests
{
    public class RegisterTests : AggregateTestHelper
    {
        private readonly Book _book;
        private readonly LibraryUser _libraryUser;

        public RegisterTests()
        {
            _libraryUser = GetValidLibraryUserAggregate();
            _book = GetValidBookAggregate();
        }

        private void Act() 
            => _book.Register(_libraryUser);

        [Fact]
        public void given_new_book_should_be_added()
        {
            // Act
            Act();

            // Assert
            _book.InStock.Should().BeTrue();
            _book.DomainEvents.Single().Should().BeOfType<NewBookRegisteredEvent>();
        }
    }
}
