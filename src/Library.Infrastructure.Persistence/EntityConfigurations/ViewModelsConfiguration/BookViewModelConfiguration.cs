using Library.Application.UseCases.Storages.ViewModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Library.Infrastructure.Persistence.EntityConfigurations.ViewModelsConfiguration
{
    public class BookViewModelConfiguration : IEntityTypeConfiguration<BookViewModel>
    {
        public void Configure(EntityTypeBuilder<BookViewModel> viewModel)
        {
            viewModel.ToTable("Book");
            viewModel.HasKey(e => e.Id);

            viewModel.Property(b => b.Title).HasColumnName("Title");
            viewModel.Property(b => b.Author).HasColumnName("Author");
            viewModel.Property(b => b.Subject).HasColumnName("Subject");
            viewModel.Property(b => b.Isbn).HasColumnName("Isbn");
            viewModel.Property(b => b.InStock).HasColumnName("InStock");
        }
    }
}