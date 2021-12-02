using Ardalis.Specification;

namespace Library.Domain.AggregateModels.BookAggregate.Specifications
{
    public sealed class BookByIsbnSpec : Specification<Book>, ISingleResultSpecification
    {
        public BookByIsbnSpec(Isbn isbn)
        {
            Query.Where(book => book.BookInformation.Isbn == isbn);
        }
    }
}