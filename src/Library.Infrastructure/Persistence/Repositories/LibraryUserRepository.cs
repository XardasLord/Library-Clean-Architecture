using System.Threading.Tasks;
using Library.Domain.AggregateModels.LibraryUserAggregate;
using Microsoft.EntityFrameworkCore;

namespace Library.Infrastructure.Persistence.Repositories
{
    public class LibraryUserRepository : ILibraryUserRepository
    {
        private readonly LibraryDbContext _dbContext;

        public LibraryUserRepository(LibraryDbContext dbContext) 
            => _dbContext = dbContext;

        public Task<LibraryUser> GetAsync(long id)
            => _dbContext.LibraryUsers.SingleOrDefaultAsync(x => x.Id == id);

        public Task<LibraryUser> GetAsync(string login)
            => _dbContext.LibraryUsers.SingleOrDefaultAsync(x => x.Login == login);

        public Task<bool> ExistsAsync(string email)
            => _dbContext.LibraryUsers.AnyAsync(x => x.Email == email);

        public Task AddAsync(LibraryUser libraryUser)
        {
            _dbContext.LibraryUsers.Add(libraryUser);
            return Task.CompletedTask;
        }

        public Task SaveChangesAsync() => _dbContext.SaveEntitiesAsync();
    }
}
