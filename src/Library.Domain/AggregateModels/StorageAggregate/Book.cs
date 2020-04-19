using Library.Domain.Exceptions;
using Library.Domain.SeedWork;

namespace Library.Domain.AggregateModels.StorageAggregate
{
    public class Book : Entity<long>
    {
        private BookInformation _bookInformation;
        private bool _inStock;

        public BookInformation BookInformation => _bookInformation;
        public bool InStock => _inStock;

        private Book() { }

        private Book(BookInformation bookInformation)
        {
            _bookInformation = bookInformation;
            _inStock = true;
        }

        public static Book Create(string title, string author, string subject, string isbn)
        {
            var bookInformation = new BookInformation(title, author, subject, isbn);
            var book = new Book(bookInformation);

            return book;
        }

        public void MarkAsUnavailable()
        {
            if (!InStock)
                throw new BookIsNotInStockException();

            _inStock = false;
        }

        public void MarkAsAvailable()
        {
            if (InStock)
                throw new BookIsInStockException(Id);

            _inStock = true;
        }
    }
}
