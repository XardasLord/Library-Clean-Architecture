using Library.Application.UseCases.Storages.ViewModels;
using Library.Infrastructure.Persistence.EntityConfigurations.ViewModelsConfiguration;
using Microsoft.EntityFrameworkCore;

namespace Library.Infrastructure.Persistence.DbContexts
{
    public class LibraryViewDbContext : DbContext
    {
        public LibraryViewDbContext(DbContextOptions options) : base(options)
        {
        }
         
        public DbSet<StorageViewModel> StorageViewModel { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
            => modelBuilder
                .ApplyConfiguration(new StorageViewModelConfiguration())
                .ApplyConfiguration(new BookViewModelConfiguration());
    }
}
