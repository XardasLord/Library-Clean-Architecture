using HotChocolate.Types;
using Library.Application.UseCases.Books.ViewModels;

namespace Library.Infrastructure.Persistence.GraphQL.Types
{
    public class StorageViewModelType : ObjectType<StorageViewModel>
    {
        protected override void Configure(IObjectTypeDescriptor<StorageViewModel> descriptor)
        {
        }
    }
}