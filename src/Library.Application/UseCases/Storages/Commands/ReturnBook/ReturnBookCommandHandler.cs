using System.Threading;
using System.Threading.Tasks;
using Library.Application.Auth;
using Library.Application.Configurations;
using Library.Domain.AggregateModels.StorageAggregate;
using MediatR;
using Microsoft.Extensions.Options;

namespace Library.Application.UseCases.Storages.Commands.ReturnBook
{
    public class ReturnBookCommandHandler : IRequestHandler<ReturnBookCommand>
    {
        private readonly IStorageRepository _storageRepository;
        private readonly ICurrentUser _currentUser;
        private readonly StorageConfig _storageConfig;

        public ReturnBookCommandHandler(IStorageRepository storageRepository, IOptions<StorageConfig> storageOptions, ICurrentUser currentUser)
        {
            _storageRepository = storageRepository;
            _currentUser = currentUser;
            _storageConfig = storageOptions.Value;
        }

        public async Task<Unit> Handle(ReturnBookCommand command, CancellationToken cancellationToken)
        {
            var storage = await _storageRepository.GetAsync(_storageConfig.DevelopStorageId);

            storage.ReturnBook(command.BookId, _currentUser.UserId);

            await _storageRepository.SaveChangesAsync();

            return Unit.Value;
        }
    }
}