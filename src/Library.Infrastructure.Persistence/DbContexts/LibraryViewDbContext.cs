using System.Reflection;
using Microsoft.EntityFrameworkCore;

namespace Library.Infrastructure.Persistence.DbContexts
{
    public class LibraryViewDbContext : DbContext
    {
        public LibraryViewDbContext(DbContextOptions options) : base(options)
        {
        }
         
        // TODO: View DbSets

        protected override void OnModelCreating(ModelBuilder modelBuilder)
            => modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
    }
}
