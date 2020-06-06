using HotChocolate.Types;
using Library.Domain.AggregateModels.StorageAggregate;

namespace Library.Infrastructure.Persistence.GraphQL.Types
{
    public class BookType : ObjectType<Book>
    {
        protected override void Configure(IObjectTypeDescriptor<Book> descriptor)
        {
            descriptor.Ignore(x => x.DomainEvents);
        }
    }
}