using System.Threading.Tasks;

namespace Library.Domain.AggregateModels.LibraryUserAggregate
{
    public interface ILibraryUserRepository
    {
        Task<LibraryUser> GetAsync(long id);
        Task<bool> ExistsAsync(string email);
        Task AddAsync(LibraryUser libraryUser);
        Task SaveChangesAsync();
    }
}
