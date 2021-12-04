using HotChocolate.Types;
using Library.Infrastructure.Persistence.DbContexts.ReadModels;

namespace Library.Infrastructure.Persistence.GraphQL.Types
{
    public class BookReadModelType : ObjectType<BookReadModel>
    {
        protected override void Configure(IObjectTypeDescriptor<BookReadModel> descriptor)
        {
        }
    }
}