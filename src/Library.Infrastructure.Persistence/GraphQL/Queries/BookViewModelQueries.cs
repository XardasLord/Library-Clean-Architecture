using System.Linq;
using HotChocolate;
using HotChocolate.Data;
using HotChocolate.Types;
using Library.Application.UseCases.Books.ViewModels;
using Library.Infrastructure.Persistence.DbContexts;

namespace Library.Infrastructure.Persistence.GraphQL.Queries
{
    public class BookViewModelQueries
    {
        [UsePaging(IncludeTotalCount = true, MaxPageSize = 200)]
        [UseProjection]
        [UseFiltering]
        [UseSorting]
        public IQueryable<BookViewModel> GetBooks([Service] LibraryDbContext dbContext)
            => dbContext.BookViewModels.AsQueryable();
    }
}
