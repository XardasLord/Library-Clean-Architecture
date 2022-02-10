using Ardalis.Specification.EntityFrameworkCore;
using Library.Domain.SeedWork;
using Library.Domain.SharedKernel;
using Library.Infrastructure.Persistence.DbContexts;

namespace Library.Infrastructure.Persistence.Repositories
{
    public class AggregateRepository<T> : RepositoryBase<T>, IAggregateReadRepository<T>, IAggregateRepository<T> where T : class, IAggregateRoot
    {
        public AggregateRepository(WriteDbContext writeDbContext)
            : base(writeDbContext)
        {
        }
    }
}