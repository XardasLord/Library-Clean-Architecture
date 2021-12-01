﻿using Ardalis.Specification;
using Ardalis.Specification.EntityFrameworkCore;
using Library.Domain.SeedWork;
using Library.Domain.SharedKernel;
using Library.Infrastructure.Persistence.DbContexts;

namespace Library.Infrastructure.Persistence.Repositories
{
    public class AggregateRepository<T> : RepositoryBase<T>, IAggregateRepository<T> where T : class, IAggregateRoot
    {
        public AggregateRepository(LibraryDbContext dbContext) 
            : base(dbContext)
        {
        }

        public AggregateRepository(LibraryDbContext dbContext, ISpecificationEvaluator specificationEvaluator) 
            : base(dbContext, specificationEvaluator)
        {
        }
    }
}