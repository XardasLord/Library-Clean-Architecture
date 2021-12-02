using System.Reflection;
using System.Threading;
using System.Threading.Tasks;
using Library.Application.UseCases.Books.ViewModels;
using Library.Domain.AggregateModels.BookAggregate;
using Library.Domain.AggregateModels.LibraryUserAggregate;
using Library.Infrastructure.Persistence.Extensions;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Library.Infrastructure.Persistence.DbContexts
{
    public class LibraryDbContext : DbContext
    {
        private readonly IMediator _mediator;

        public LibraryDbContext(DbContextOptions options, IMediator mediator) : base(options)
        {
            _mediator = mediator;
        }

        public DbSet<Book> Books { get; set; }
        public DbSet<LibraryUser> LibraryUsers { get; set; }
        
        // For GraphQL queries
        public DbSet<BookViewModel> BookViewModels { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
            => modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
        {
            var result = await base.SaveChangesAsync(cancellationToken);
            
            await _mediator.DispatchDomainEventsAsync(this);

            return result;
        }
    }
}
