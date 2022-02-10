using Library.Domain.AggregateModels.BookAggregate;
using Library.Infrastructure.Persistence.DbContexts.ReadModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Library.Infrastructure.Persistence.DbContexts.EntityConfigurations.ReadModelsConfiguration
{
    public class BookReadModelConfiguration : IEntityTypeConfiguration<BookReadModel>
    {
        public void Configure(EntityTypeBuilder<BookReadModel> readModel)
        {
            readModel.ToTable("Book");
            readModel.HasKey(x => x.Id);

            readModel.Property(x => x.Id).HasColumnName("BookId");
            readModel.Property(x => x.Title).HasColumnName("Title");
            readModel.Property(x => x.Author).HasColumnName("Author");
            readModel.Property(x => x.Subject).HasColumnName("Subject");
            readModel.Property(x => x.Isbn).HasColumnName("Isbn");
            readModel.Property(x => x.InStock).HasColumnName("InStock");
            
            readModel
                .HasOne<Book>()
                .WithOne()
                .HasForeignKey<Book>(x => x.Id);
        }
    }
}