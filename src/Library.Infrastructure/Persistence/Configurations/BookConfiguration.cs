using Library.Domain.AggregateModels.BookAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Library.Infrastructure.Persistence.Configurations
{
    public class BookConfiguration : IEntityTypeConfiguration<Book>
    {
        public void Configure(EntityTypeBuilder<Book> builder)
        {
            builder.ToTable("Book");
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id).HasColumnName("BookId");
            builder.Property(x => x.InStock).HasColumnName("InStock");

            builder.OwnsOne(x => x.BookInformation, x =>
            {
                x.Property(b => b.Title).HasColumnName("Title");
                x.Property(b => b.Author).HasColumnName("Author");
                x.Property(b => b.Subject).HasColumnName("Subject");
                x.Property(b => b.Isbn).HasColumnName("Isbn");
            });
        }
    }
}
