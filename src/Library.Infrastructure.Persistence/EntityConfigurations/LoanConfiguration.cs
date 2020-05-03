using Library.Domain.AggregateModels.LibraryUserAggregate;
using Library.Domain.AggregateModels.StorageAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Library.Infrastructure.Persistence.EntityConfigurations
{
    public class LoanConfiguration : IEntityTypeConfiguration<Loan>
    {
        public void Configure(EntityTypeBuilder<Loan> entity)
        {
            entity.ToTable("Loan");
            entity.HasKey(x => x.Id);

            entity.Ignore(e => e.DomainEvents);

            entity.Property(e => e.Id)
                .HasColumnName("LoanId")
                .UseIdentityColumn();

            entity.Property(e => e.Active)
                .HasColumnName("Active")
                .IsRequired();

            entity.OwnsOne(e => e.DateTimePeriod, x =>
            {
                x.Property(d => d.StartDate).HasColumnName("StartDate").IsRequired();
                x.Property(d => d.EndDate).HasColumnName("EndDate").IsRequired();
            });

            entity.HasOne<Book>("_book")
                .WithMany()
                .HasForeignKey(x => x.BookId);

            entity.HasOne<LibraryUser>("_user")
                .WithMany()
                .HasForeignKey(x => x.UserId);
        }
    }
}
