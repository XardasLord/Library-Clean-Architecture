using System.Threading;
using System.Threading.Tasks;
using Library.Application.Auth;
using Library.Application.Configurations;
using Library.Domain.AggregateModels.StorageAggregate;
using MediatR;
using Microsoft.Extensions.Options;

namespace Library.Application.UseCases.Storages.Commands.BorrowBook
{
    public class BorrowBookCommandHandler : IRequestHandler<BorrowBookCommand>
    {
        private readonly IStorageRepository _storageRepository;
        private readonly ITokenAuthInfo _tokenAuthInfo;
        private readonly StorageConfig _storageConfig;

        public BorrowBookCommandHandler(IStorageRepository storageRepository, IOptions<StorageConfig> storageOptions, ITokenAuthInfo tokenAuthInfo)
        {
            _storageRepository = storageRepository;
            _tokenAuthInfo = tokenAuthInfo;
            _storageConfig = storageOptions.Value;
        }

        public async Task<Unit> Handle(BorrowBookCommand command, CancellationToken cancellationToken)
        {
            var storage = await _storageRepository.GetAsync(_storageConfig.DevelopStorageId);

            var dateTimePeriod = DateTimePeriod.Create(command.BorrowingStartDate, command.BorrowingEndDate);

            storage.BorrowBook(command.BookId, _tokenAuthInfo.UserId, dateTimePeriod);

            await _storageRepository.SaveChangesAsync();

            return Unit.Value;
        }
    }
}