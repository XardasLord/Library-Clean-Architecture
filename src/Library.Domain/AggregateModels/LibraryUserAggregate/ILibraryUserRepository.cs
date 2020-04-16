using System.Threading.Tasks;

namespace Library.Domain.AggregateModels.LibraryUserAggregate
{
    public interface ILibraryUserRepository
    {
        Task<LibraryUser> GetAsync(long id);
        Task<LibraryUser> GetAsync(string login);
        Task<bool> ExistsAsync(string email);
        Task AddAsync(LibraryUser libraryUser);
        Task SaveChangesAsync();
    }
}
