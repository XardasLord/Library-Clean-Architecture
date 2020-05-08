using System.Linq;
using System.Threading.Tasks;
using Library.Domain.AggregateModels.StorageAggregate;
using Microsoft.EntityFrameworkCore;

namespace Library.Infrastructure.Persistence.Repositories
{
    public class StorageRepository : IStorageRepository
    {
        private readonly LibraryDbContext _dbContext;

        public StorageRepository(LibraryDbContext dbContext) 
            => _dbContext = dbContext;

        public Task<Storage> GetAsync(long storageId) 
            => _dbContext
                .Storages
                .Where(x => x.Id == storageId)
                .Include(x => x.Books)
                .Include(x => x.Loans)
                .SingleOrDefaultAsync();

        public Task SaveChangesAsync()
            => _dbContext.SaveEntitiesAsync();
    }
}
