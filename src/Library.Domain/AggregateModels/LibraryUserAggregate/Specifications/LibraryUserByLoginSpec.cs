using Ardalis.Specification;

namespace Library.Domain.AggregateModels.LibraryUserAggregate.Specifications
{
    public sealed class LibraryUserByLoginSpec : Specification<LibraryUser>, ISingleResultSpecification
    {
        public LibraryUserByLoginSpec(string login)
        {
            Query
                .Where(user => user.Credentials.Login == login);
        }
    }
}