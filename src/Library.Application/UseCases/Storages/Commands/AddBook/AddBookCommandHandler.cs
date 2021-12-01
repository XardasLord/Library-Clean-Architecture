using System.Threading;
using System.Threading.Tasks;
using Library.Application.Configurations;
using Library.Domain.AggregateModels.StorageAggregate;
using Library.Domain.AggregateModels.StorageAggregate.Specifications;
using Library.Domain.SharedKernel;
using MediatR;
using Microsoft.Extensions.Options;

namespace Library.Application.UseCases.Storages.Commands.AddBook
{
    public class AddBookCommandHandler : IRequestHandler<AddBookCommand, long>
    {
        private readonly IAggregateRepository<Storage> _storageRepository;
        private readonly StorageConfig _storageConfig;

        public AddBookCommandHandler(IAggregateRepository<Storage> storageRepository, IOptions<StorageConfig> storageOptions)
        {
            _storageRepository = storageRepository;
            _storageConfig = storageOptions.Value;
        }

        public async Task<long> Handle(AddBookCommand command, CancellationToken cancellationToken)
        {
            var spec = new StorageWithBooksAndLoansSpec(_storageConfig.DevelopStorageId);
            var storage = await _storageRepository.GetBySpecAsync(spec, cancellationToken);

            var book = Book.Create(command.Title, command.Author, command.Subject, command.Isbn);
            storage.AddBook(book);

            await _storageRepository.SaveChangesAsync(cancellationToken);

            return book.Id;
        }
    }
}