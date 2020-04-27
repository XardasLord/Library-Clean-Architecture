using Library.Domain.AggregateModels.StorageAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Library.Infrastructure.Persistence.EntityConfigurations
{
    public class StorageConfiguration : IEntityTypeConfiguration<Storage>
    {
        public void Configure(EntityTypeBuilder<Storage> entity)
        {
            entity.ToTable("Storage");
            entity.HasKey(e => e.Id);

            entity.Ignore(e => e.DomainEvents);
            entity.Ignore(e => e.AvailableBooks);

            entity.HasMany(e => e.Books)
                .WithOne();

            entity.HasMany(e => e.Loans);
        }
    }
}
