using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Library.Domain.AggregateModels.BookAggregate;
using Microsoft.EntityFrameworkCore;

namespace Library.Infrastructure.Persistence.Repositories
{
    public class BookRepository : IBookRepository
    {
        private readonly LibraryDbContext _dbContext;

        public BookRepository(LibraryDbContext dbContext) 
            => _dbContext = dbContext;

        public Task<Book> GetAsync(long id) 
            => _dbContext.Books.SingleOrDefaultAsync(book => book.Id == id);

        public Task<List<Book>> GetAllAvailableAsync()
            => _dbContext.Books.Where(book => book.InStock).ToListAsync();

        public Task AddAsync(Book book)
        {
            _dbContext.Books.Add(book);
            return Task.CompletedTask;
        }

        public Task SaveChangesAsync() => _dbContext.SaveEntitiesAsync();
    }
}
