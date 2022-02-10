using System;
using Ardalis.Specification;

namespace Library.Domain.AggregateModels.LibraryUserAggregate.Specifications
{
    public sealed class LibraryUserByEmailSpec : Specification<LibraryUser>, ISingleResultSpecification
    {
        public LibraryUserByEmailSpec(string email)
        {
            Query
                .Where(user => user.Email.Value == email);
        }
    }
}