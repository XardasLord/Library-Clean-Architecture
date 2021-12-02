using Library.Application.UseCases.Books.ViewModels;
using Library.Domain.AggregateModels.BookAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Library.Infrastructure.Persistence.EntityConfigurations.ViewModelsConfiguration
{
    public class BookViewModelConfiguration : IEntityTypeConfiguration<BookViewModel>
    {
        public void Configure(EntityTypeBuilder<BookViewModel> viewModel)
        {
            viewModel.ToTable("Book"); // TODO: View maybe instead?
            viewModel.HasKey(e => e.Id);
            
            viewModel.Ignore(x => x.InStock);

            viewModel.Property(b => b.Title).HasColumnName("Title");
            viewModel.Property(b => b.Author).HasColumnName("Author");
            viewModel.Property(b => b.Subject).HasColumnName("Subject");
            viewModel.Property(b => b.Isbn).HasColumnName("Isbn");
            
            viewModel
                .HasOne<Book>()
                .WithOne()
                .HasForeignKey<BookViewModel>(e => e.Id);
        }
    }
}