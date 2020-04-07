using Library.Domain.AggregateModels.BookAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Library.Infrastructure.Persistence.EntityConfigurations
{
    public class BookConfiguration : IEntityTypeConfiguration<Book>
    {
        public void Configure(EntityTypeBuilder<Book> entity)
        {
            entity.ToTable("Book");
            entity.HasKey(x => x.Id);

            entity.Ignore(x => x.DomainEvents);

            entity.Property(x => x.Id)
                .HasColumnName("BookId")
                .UseIdentityColumn();

            entity.Property(x => x.InStock).HasColumnName("InStock");

            entity.OwnsOne(x => x.BookInformation, x =>
            {
                x.Property(b => b.Title).HasColumnName("Title");
                x.Property(b => b.Author).HasColumnName("Author");
                x.Property(b => b.Subject).HasColumnName("Subject");
                x.Property(b => b.Isbn).HasColumnName("Isbn");
            });
        }
    }
}
