using Ardalis.Specification;
using Library.Domain.SeedWork;

namespace Library.Domain.SharedKernel
{
    public interface IAggregateRepository<T> : IRepositoryBase<T> where T : class, IAggregateRoot
    {
    }
}