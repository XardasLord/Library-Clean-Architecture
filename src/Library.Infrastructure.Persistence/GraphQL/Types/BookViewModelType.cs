using HotChocolate.Types;
using Library.Application.UseCases.Books.ViewModels;

namespace Library.Infrastructure.Persistence.GraphQL.Types
{
    public class BookViewModelType : ObjectType<BookViewModel>
    {
        protected override void Configure(IObjectTypeDescriptor<BookViewModel> descriptor)
        {
        }
    }
}