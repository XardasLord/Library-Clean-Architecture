using System.Threading;
using System.Threading.Tasks;
using Library.Domain.AggregateModels.BookAggregate;
using Library.Domain.AggregateModels.LibraryUserAggregate;
using Library.Infrastructure.Persistence.DbContexts.EntityConfigurations;
using Library.Infrastructure.Persistence.Extensions;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Library.Infrastructure.Persistence.DbContexts
{
    public class WriteDbContext : DbContext
    {
        private readonly IMediator _mediator;

        public WriteDbContext(DbContextOptions<WriteDbContext> options, IMediator mediator) : base(options)
        {
            _mediator = mediator;
        }

        public DbSet<Book> Books { get; set; }
        public DbSet<LibraryUser> LibraryUsers { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
            => modelBuilder
                .ApplyConfiguration(new BookConfiguration())
                .ApplyConfiguration(new LibraryUserConfiguration())
                .ApplyConfiguration(new LoanConfiguration());

        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = new())
        {
            var result = await base.SaveChangesAsync(cancellationToken);
            
            await _mediator.DispatchDomainEventsAsync(this);

            return result;
        }
    }
}
