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
        public IQueryable<StorageViewModel> GetStorageInformation([Service] LibraryViewDbContext context, long storageId)
            => context.StorageViewModel.Where(x => x.Id == storageId);
    }
}
