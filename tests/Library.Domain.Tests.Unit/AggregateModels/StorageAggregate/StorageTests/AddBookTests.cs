using System.Linq;
using FluentAssertions;
using Library.Domain.AggregateModels.StorageAggregate;
using Library.Domain.AggregateModels.StorageAggregate.Events;
using Xunit;

namespace Library.Domain.Tests.Unit.AggregateModels.StorageAggregate.StorageTests
{
    public class AddBookTests
    {
        private Storage _storage;

        internal void Act(Book book) => _storage.AddBook(book);

        [Fact]
        public void given_new_book_should_be_added()
        {
            _storage = Storage.Create(1);
            const string title = "Book Title";
            const string author = "Book Author";
            const string subject = "Subject";
            const string isbn = "9783161484100";
            var newBook = Book.Create(title, author, subject, isbn);

            Act(newBook);

            _storage.Books.Should().HaveCount(1);
            _storage.AvailableBooks.Should().HaveCount(1);
            _storage.Books.Single().Should().Be(newBook);
            _storage.Loans.Should().HaveCount(0);
            _storage.DomainEvents.Should().HaveCount(1);
            _storage.DomainEvents.Single().Should().BeOfType<BookAddedToStorageEvent>();
        }
    }
}
