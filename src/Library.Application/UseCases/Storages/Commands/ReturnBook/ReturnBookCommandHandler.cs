using System.Threading;
using System.Threading.Tasks;
using Library.Application.Auth;
using Library.Application.Configurations;
using Library.Domain.AggregateModels.StorageAggregate;
using Library.Domain.AggregateModels.StorageAggregate.Specifications;
using Library.Domain.SharedKernel;
using MediatR;
using Microsoft.Extensions.Options;

namespace Library.Application.UseCases.Storages.Commands.ReturnBook
{
    public class ReturnBookCommandHandler : IRequestHandler<ReturnBookCommand>
    {
        private readonly IAggregateRepository<Storage> _storageRepository;
        private readonly ICurrentUser _currentUser;
        private readonly StorageConfig _storageConfig;

        public ReturnBookCommandHandler(
            IAggregateRepository<Storage> storageRepository,
            IOptions<StorageConfig> storageOptions,
            ICurrentUser currentUser)
        {
            _storageRepository = storageRepository;
            _currentUser = currentUser;
            _storageConfig = storageOptions.Value;
        }

        public async Task<Unit> Handle(ReturnBookCommand command, CancellationToken cancellationToken)
        {
            var spec = new StorageWithBooksAndLoansSpec(_storageConfig.DevelopStorageId);
            var storage = await _storageRepository.GetBySpecAsync(spec, cancellationToken);

            storage.ReturnBook(command.BookId, _currentUser.UserId);

            await _storageRepository.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}