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

    public sealed class LibraryUserWithActiveLoansSpec : Specification<LibraryUser>, ISingleResultSpecification
    {
        public LibraryUserWithActiveLoansSpec(long id)
        {
            Query
                .Include(user => user.ActiveLoans)
                .Where(user => user.Id == id);
        }
    }
}