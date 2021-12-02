using System.Linq;
using HotChocolate;
using HotChocolate.Types;
using HotChocolate.Types.Relay;
using Library.Application.UseCases.Books.ViewModels;
using Library.Infrastructure.Persistence.DbContexts;

namespace Library.Infrastructure.Persistence.GraphQL.Queries
{
    public class StorageViewModelQuery
    {
        [UsePaging]
        [UseFiltering]
        [UseSorting]
        [UseSelection]
        public IQueryable<BookViewModel> GetBooks([Service] LibraryViewDbContext viewContext)
            => viewContext.BookViewModels.AsQueryable();
    }
}
