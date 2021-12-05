using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace Library.Infrastructure.Persistence.DbContexts.Factories
{
    public class LibraryDbContextFactory : IDesignTimeDbContextFactory<WriteDbContext>
    {
        public WriteDbContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<WriteDbContext>();
            optionsBuilder.UseSqlServer("FOR_MIGRATION_PURPOSES_ONLY");

            return new WriteDbContext(optionsBuilder.Options, null);
        }
    }
}
