﻿using System.Reflection;
using System.Threading;
using System.Threading.Tasks;
using Library.Domain.AggregateModels.BookAggregate;
using Library.Infrastructure.Extensions;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Library.Infrastructure.Persistence
{
    public class LibraryDbContext : DbContext
    {
        private readonly IMediator _mediator;

        public LibraryDbContext(DbContextOptions options, IMediator mediator) : base(options) 
            => _mediator = mediator;

        public DbSet<Book> Books { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder) 
            => modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

        public async Task SaveEntitiesAsync()
        {
            await base.SaveChangesAsync(CancellationToken.None);

            await _mediator.DispatchDomainEventsAsync(this);
        }
    }
}
