using System.Linq;
using HotChocolate;
using HotChocolate.Data;
using HotChocolate.Types;
using Library.Infrastructure.Persistence.DbContexts;
using Library.Infrastructure.Persistence.DbContexts.ReadModels;

namespace Library.Infrastructure.Persistence.GraphQL.Queries
{
    public class BookReadModelQueries
    {
        [UsePaging(IncludeTotalCount = true, MaxPageSize = 200)]
        [UseProjection]
        [UseFiltering]
        [UseSorting]
        public IQueryable<BookReadModel> GetBooks([Service] LibraryReadDbContext dbContext)
            => dbContext.BookReadModels.AsQueryable();
    }
}
