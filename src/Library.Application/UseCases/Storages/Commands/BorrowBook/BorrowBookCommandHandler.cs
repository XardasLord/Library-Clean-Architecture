using System;
using System.Threading;
using System.Threading.Tasks;
using Library.Application.Configurations;
using Library.Domain.AggregateModels.StorageAggregate;
using MediatR;
using Microsoft.Extensions.Options;

namespace Library.Application.UseCases.Storages.Commands.BorrowBook
{
    public class BorrowBookCommandHandler : IRequestHandler<BorrowBookCommand>
    {
        private readonly IStorageRepository _storageRepository;
        private readonly StorageConfig _storageConfig;

        public BorrowBookCommandHandler(IStorageRepository storageRepository, IOptions<StorageConfig> storageOptions)
        {
            _storageRepository = storageRepository;
            _storageConfig = storageOptions.Value;
        }

        public async Task<Unit> Handle(BorrowBookCommand command, CancellationToken cancellationToken)
        {
            var storage = await _storageRepository.GetAsync(_storageConfig.DevelopStorageId);

            // TODO: FromDate can be passed in command. We can reserve the book borrowing in the future.
            storage.BorrowBook(command.BookId, command.UserId, DateTime.UtcNow, command.BorrowingEndDate);

            await _storageRepository.SaveChangesAsync();

            return Unit.Value;
        }
    }
}