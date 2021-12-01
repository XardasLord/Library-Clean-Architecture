using Ardalis.Specification;

namespace Library.Domain.AggregateModels.StorageAggregate.Specifications
{
    public sealed class StorageWithBooksAndLoansSpec : Specification<Storage>, ISingleResultSpecification
    {
        public StorageWithBooksAndLoansSpec(long id)
        {
            Query
                .Where(storage => storage.Id == id)
                .Include(storage => storage.Books)
                .Include(storage => storage.Loans);
        }
    }
}