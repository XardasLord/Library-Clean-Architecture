using Ardalis.Specification;

namespace Library.Domain.AggregateModels.BookAggregate.Specifications
{
    public sealed class AvailableBooksSpec : Specification<Book>
    {
        public AvailableBooksSpec()
        {
            Query
                .Where(book => book.InStock);
        }
    }
}