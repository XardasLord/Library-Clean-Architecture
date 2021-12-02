using Library.Domain.SharedKernel;
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

            entity
                .Property(e => e.Id)
                .HasColumnName("LoanId")
                .UseIdentityColumn();

            entity
                .Property(e => e.IsActive)
                .HasColumnName("IsActive")
                .IsRequired();

            entity.OwnsOne(e => e.BorrowPeriod, x =>
            {
                x.Property(d => d.StartDate).HasColumnName("StartDate").IsRequired();
                x.Property(d => d.EndDate).HasColumnName("EndDate").IsRequired();
            });

            entity
                .Property<long>("_bookId")
                .UsePropertyAccessMode(PropertyAccessMode.Field)
                .HasColumnName("BookId")
                .IsRequired();
            
            entity
                .Property<long>("_userId")
                .UsePropertyAccessMode(PropertyAccessMode.Field)
                .HasColumnName("UserId")
                .IsRequired();
        }
    }
}
