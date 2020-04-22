using System.Collections.Generic;
using System.Threading.Tasks;

namespace Library.Domain.AggregateModels.StorageAggregate
{
    // TODO: Remove it.
    // TODO: Create IStorageRepository instead
    public interface IBookRepository
    {
        Task<Book> GetAsync(long id);
        Task<List<Book>> GetAllAvailableAsync();
        Task AddAsync(Book book);
        Task SaveChangesAsync();
    }
}
