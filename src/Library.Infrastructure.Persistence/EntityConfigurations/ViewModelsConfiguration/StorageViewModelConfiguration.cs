using Library.Application.UseCases.Books.ViewModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Library.Infrastructure.Persistence.EntityConfigurations.ViewModelsConfiguration
{
    public class StorageViewModelConfiguration : IEntityTypeConfiguration<StorageViewModel>
    {
        public void Configure(EntityTypeBuilder<StorageViewModel> viewModel)
        {
            viewModel.ToTable("Storage");
            viewModel.HasKey(e => e.Id);

            viewModel.HasMany(e => e.Books)
                .WithOne();
        }
    }
}