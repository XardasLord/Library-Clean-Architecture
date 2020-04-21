using System.Threading;
using System.Threading.Tasks;
using Library.Application.Configurations;
using Library.Domain.AggregateModels.StorageAggregate;
using MediatR;
using Microsoft.Extensions.Options;

namespace Library.Application.UseCases.Storages.Commands.AddBook
{
    public class AddBookCommandHandler : IRequestHandler<AddBookCommand, long>
    {
        private readonly IStorageRepository _storageRepository;
        private readonly StorageConfig _storageConfig;

        public AddBookCommandHandler(IStorageRepository storageRepository, IOptions<StorageConfig> storageOptions)
        {
            _storageRepository = storageRepository;
            _storageConfig = storageOptions.Value;
        }

        public async Task<long> Handle(AddBookCommand command, CancellationToken cancellationToken)
        {
            var storage = await _storageRepository.GetAsync(_storageConfig.DevelopStorageId);

            var book = Book.Create(command.Title, command.Author, command.Subject, command.Isbn);
            storage.AddBook(book);

            await _storageRepository.SaveChangesAsync();

            return book.Id;
        }
    }
}