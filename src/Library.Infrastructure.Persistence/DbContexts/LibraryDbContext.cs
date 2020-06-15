using System.Threading;
using System.Threading.Tasks;
using Library.Domain.AggregateModels.LibraryUserAggregate;
using Library.Domain.AggregateModels.StorageAggregate;
using Library.Infrastructure.Persistence.EntityConfigurations;
using Library.Infrastructure.Persistence.Extensions;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Library.Infrastructure.Persistence.DbContexts
{
    public class LibraryDbContext : DbContext
    {
        private readonly IMediator _mediator;

        public LibraryDbContext(DbContextOptions options, IMediator mediator) : base(options) 
            => _mediator = mediator;

        public DbSet<Book> Books { get; set; }
        public DbSet<LibraryUser> LibraryUsers { get; set; }
        public DbSet<Storage> Storages { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
            => modelBuilder
                .ApplyConfiguration(new StorageConfiguration())
                .ApplyConfiguration(new BookConfiguration())
                .ApplyConfiguration(new LibraryUserConfiguration())
                .ApplyConfiguration(new LoanConfiguration());

        public async Task SaveEntitiesAsync()
        {
            await base.SaveChangesAsync(CancellationToken.None);

            await _mediator.DispatchDomainEventsAsync(this);
        }
    }
}
