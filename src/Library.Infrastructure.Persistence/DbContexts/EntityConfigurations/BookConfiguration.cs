using Library.Domain.AggregateModels.BookAggregate;
using Library.Domain.SharedKernel;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Library.Infrastructure.Persistence.DbContexts.EntityConfigurations
{
    public class BookConfiguration : IEntityTypeConfiguration<Book>
    {
        public void Configure(EntityTypeBuilder<Book> entity)
        {
            entity.ToTable("Book");
            entity.HasKey(x => x.Id);

            entity.Ignore(x => x.DomainEvents);
            entity.Ignore(x => x.InStock);

            entity.Property(x => x.Id)
                .HasColumnName("BookId")
                .HasConversion(id => id.Value, id => new BookId(id))
                .UseIdentityColumn();

            entity.Property(x => x.InStock)
                .HasColumnName("InStock")
                .IsRequired();

            entity.OwnsOne(x => x.BookInformation, x =>
            {
                x.Property(b => b.Title)
                    .HasColumnName("Title")
                    .IsRequired();

                x.Property(b => b.Author)
                    .HasColumnName("Author")
                    .IsRequired();
                x.Property(b => b.Subject)
                    .HasColumnName("Subject")
                    .IsRequired();

                x.OwnsOne(b => b.Isbn, b =>
                {
                    b.Property(i => i.Value)
                        .HasColumnName("Isbn")
                        .IsRequired();
                });
            });
            
            entity.HasMany<Loan>("_loans")
                .WithOne()
                .IsRequired(false)
                .HasForeignKey("_bookId")
                .Metadata.PrincipalToDependent.SetPropertyAccessMode(PropertyAccessMode.Field);
        }
    }
}
