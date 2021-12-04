using Library.Infrastructure.Persistence.DbContexts.EntityConfigurations.ReadModelsConfiguration;
using Library.Infrastructure.Persistence.DbContexts.ReadModels;
using Microsoft.EntityFrameworkCore;

namespace Library.Infrastructure.Persistence.DbContexts
{
    public class LibraryReadDbContext : DbContext
    {
        public DbSet<BookReadModel> BookReadModels => Set<BookReadModel>();

        public LibraryReadDbContext(DbContextOptions<LibraryReadDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
            => modelBuilder.ApplyConfiguration(new BookReadModelConfiguration());
    }
}