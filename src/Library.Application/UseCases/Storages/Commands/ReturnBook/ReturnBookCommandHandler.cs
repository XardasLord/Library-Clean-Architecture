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
        private readonly ITokenAuthInfo _tokenAuthInfo;
        private readonly StorageConfig _storageConfig;

        public ReturnBookCommandHandler(IStorageRepository storageRepository, IOptions<StorageConfig> storageOptions, ITokenAuthInfo tokenAuthInfo)
        {
            _storageRepository = storageRepository;
            _tokenAuthInfo = tokenAuthInfo;
            _storageConfig = storageOptions.Value;
        }

        public async Task<Unit> Handle(ReturnBookCommand command, CancellationToken cancellationToken)
        {
            var storage = await _storageRepository.GetAsync(_storageConfig.DevelopStorageId);

            storage.ReturnBook(command.BookId, _tokenAuthInfo.UserId);

            await _storageRepository.SaveChangesAsync();

            return Unit.Value;
        }
    }
}