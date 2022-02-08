using Library.Domain.AggregateModels.LibraryUserAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Library.Infrastructure.Persistence.DbContexts.EntityConfigurations
{
    public class LibraryUserConfiguration : IEntityTypeConfiguration<LibraryUser>
    {
        public void Configure(EntityTypeBuilder<LibraryUser> entity)
        {
            entity.ToTable("LibraryUser");
            entity.HasKey(x => x.Id);

            entity.Ignore(x => x.DomainEvents);

            entity.Property(x => x.Id)
                .HasColumnName("LibraryUserId")
                // .HasConversion(id => id.Value, id => new LibraryUserId(id)); // In case of custom Identity class representation
                .UseIdentityColumn();

            entity.Property(x => x.FirstName)
                .HasColumnName("FirstName")
                .IsRequired();

            entity.Property(x => x.LastName)
                .HasColumnName("LastName")
                .IsRequired();

            entity.Property(x => x.IsActive)
                .HasColumnName("IsActive");

            entity.OwnsOne(x => x.Email, x =>
            {
                x.Property(e => e.Value)
                    .HasColumnName("Email")
                    .IsRequired();
            });

            entity.OwnsOne(x => x.Credentials, x =>
            {
                x.Property(e => e.Login)
                    .HasColumnName("Login")
                    .IsRequired();

                x.Property(e => e.Password)
                    .HasColumnName("Password")
                    .IsRequired();
            });
            
            entity.HasMany(b => b.ActiveLoans)
                .WithOne()
                .HasForeignKey("_userId");
        }
    }
}