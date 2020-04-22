using System.Threading.Tasks;

namespace Library.Domain.AggregateModels.StorageAggregate
{
    public interface IStorageRepository
    {
        Task<Storage> GetAsync(long storageId);
        Task SaveChangesAsync();
    }
}
