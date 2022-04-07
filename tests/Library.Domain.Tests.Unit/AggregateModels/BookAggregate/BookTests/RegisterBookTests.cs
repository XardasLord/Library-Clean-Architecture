using System;
using System.Linq;
using FluentAssertions;
using Library.Domain.AggregateModels.BookAggregate;
using Library.Domain.AggregateModels.BookAggregate.Events;
using Library.Domain.AggregateModels.BookAggregate.Exceptions;
using Library.Domain.AggregateModels.LibraryUserAggregate;
using Library.Domain.Tests.Unit.Helpers;
using Xunit;

namespace Library.Domain.Tests.Unit.AggregateModels.BookAggregate.BookTests
{
    public class RegisterBookTests : AggregateTestHelper
    {
        private readonly LibraryUser _libraryUser;
        
        public RegisterBookTests()
        {
            _libraryUser = GetValidLibraryUserAggregate();
        }
        
        private Book Act(string title, string author, string subject, string isbn) 
            => Book.Register(title, author, subject, isbn, _libraryUser.Id);

        [Fact]
        public void given_valid_data_book_should_be_registered()
        {
            // Arrange
            var title = GetBookTitle;
            var author = GetBookAuthor;
            var subject = GetBookSubject;
            var isbn = GetIsbn.Value;

            // Act
            var result = Act(title, author, subject, isbn);

            // Assert
            result.Should().NotBeNull();
            result.BookInformation.Title.Should().Be(title);
            result.BookInformation.Author.Should().Be(author);
            result.BookInformation.Subject.Should().Be(subject);
            result.BookInformation.Isbn.Value.Should().Be(isbn);
            result.InStock.Should().BeTrue();
            result.DomainEvents.Last().Should().BeOfType<NewBookRegisteredEvent>();
        }

        [Theory]
        [MemberData(nameof(StringEmptyOrWhiteSpaceData))]
        public void given_empty_title_book_registration_should_throw_an_exception(string title)
        {
            var author = GetBookAuthor;
            var subject = GetBookSubject;
            var isbn = GetIsbn.Value;

            var result = Record.Exception(() => Act(title, author, subject, isbn));

            result.Should().NotBeNull();
            result.Should().BeOfType<ArgumentException>();
        }

        [Fact]
        public void given_null_title_book_registration_should_throw_an_exception()
        {
            var author = GetBookAuthor;
            var subject = GetBookSubject;
            var isbn = GetIsbn.Value;

            var result = Record.Exception(() => Act(null, author, subject, isbn));

            result.Should().NotBeNull();
            result.Should().BeOfType<ArgumentNullException>();
        }

        [Theory]
        [MemberData(nameof(StringEmptyOrWhiteSpaceData))]
        public void given_empty_author_book_registration_should_throw_an_exception(string author)
        {
            var title = GetBookTitle;
            var subject = GetBookSubject;
            var isbn = GetIsbn.Value;

            var result = Record.Exception(() => Act(title, author, subject, isbn));

            result.Should().NotBeNull();
            result.Should().BeOfType<ArgumentException>();
        }

        [Fact]
        public void given_null_author_book_registration_should_throw_an_exception()
        {
            var title = GetBookTitle;
            var subject = GetBookSubject;
            var isbn = GetIsbn.Value;

            var result = Record.Exception(() => Act(title, null, subject, isbn));

            result.Should().NotBeNull();
            result.Should().BeOfType<ArgumentNullException>();
        }

        [Theory]
        [MemberData(nameof(StringEmptyOrWhiteSpaceData))]
        public void given_empty_subject_book_registration_should_throw_an_exception(string subject)
        {
            var title = GetBookTitle;
            var author = GetBookAuthor;
            var isbn = GetIsbn.Value;

            var result = Record.Exception(() => Act(title, author, subject, isbn));

            result.Should().NotBeNull();
            result.Should().BeOfType<ArgumentException>();
        }

        [Fact]
        public void given_null_subject_book_registration_should_throw_an_exception()
        {
            var title = GetBookTitle;
            var author = GetBookAuthor;
            var isbn = GetIsbn.Value;

            var result = Record.Exception(() => Act(title, author, null, isbn));

            result.Should().NotBeNull();
            result.Should().BeOfType<ArgumentNullException>();
        }

        [Theory]
        [MemberData(nameof(StringEmptyOrWhiteSpaceData))]
        public void given_empty_isbn_book_registration_should_throw_an_exception(string isbn)
        {
            var title = GetBookTitle;
            var author = GetBookAuthor;
            var subject = GetBookSubject;

            var result = Record.Exception(() => Act(title, author, subject, isbn));

            result.Should().NotBeNull();
            result.Should().BeOfType<ArgumentException>();
        }

        [Fact]
        public void given_null_isbn_book_registration_should_throw_an_exception()
        {
            var title = GetBookTitle;
            var author = GetBookAuthor;
            var subject = GetBookSubject;

            var result = Record.Exception(() => Act(title, author, subject, null));

            result.Should().NotBeNull();
            result.Should().BeOfType<ArgumentNullException>();
        }

        [Theory]
        [InlineData("111222333")] // 9 length
        [InlineData("11122233344")] // 11 length
        [InlineData("111222333444")] // 12 length
        [InlineData("11122233344455")] // 14 length
        public void given_invalid_isbn_format_book_registration_should_throw_an_exception(string isbn)
        {
            var title = GetBookTitle;
            var author = GetBookAuthor;
            var subject = GetBookSubject;

            var result = Record.Exception(() => Act(title, author, subject, isbn));

            result.Should().NotBeNull();
            result.Should().BeOfType<BookIsbnInvalidFormatException>();
        }
    }
}
