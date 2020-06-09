using System.Linq;
using HotChocolate;
using HotChocolate.Types;
using Library.Domain.AggregateModels.StorageAggregate;
using Library.Infrastructure.Persistence.DbContexts;

namespace Library.Infrastructure.Persistence.GraphQL.Queries
{
    public class BookQuery
    {
        [UseSelection]
        public IQueryable<Book> GetBooks([Service] LibraryDbContext context)
            => context.Books;
    }
}
