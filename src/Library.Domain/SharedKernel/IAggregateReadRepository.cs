using Ardalis.Specification;
using Library.Domain.SeedWork;

namespace Library.Domain.SharedKernel
{
    public interface IAggregateReadRepository<T> : IReadRepositoryBase<T> where T : class, IAggregateRoot
    {
    }
}