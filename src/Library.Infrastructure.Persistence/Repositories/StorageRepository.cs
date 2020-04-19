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

        public Task<Storage> GetAsync() 
            => _dbContext
                .Storages
                .Include(x => x.Books)
                .SingleOrDefaultAsync();

        public Task SaveChangesAsync()
            => _dbContext.SaveEntitiesAsync();
    }
}
