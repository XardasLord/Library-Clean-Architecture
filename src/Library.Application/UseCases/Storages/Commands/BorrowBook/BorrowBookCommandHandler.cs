using System.Threading;
using System.Threading.Tasks;
using Library.Application.Auth;
using Library.Application.Configurations;
using Library.Domain.AggregateModels.StorageAggregate;
using Library.Domain.AggregateModels.StorageAggregate.Specifications;
using Library.Domain.SharedKernel;
using MediatR;
using Microsoft.Extensions.Options;

namespace Library.Application.UseCases.Storages.Commands.BorrowBook
{
    public class BorrowBookCommandHandler : IRequestHandler<BorrowBookCommand>
    {
        private readonly IAggregateRepository<Storage> _storageRepository;
        private readonly ICurrentUser _currentUser;
        private readonly StorageConfig _storageConfig;

        public BorrowBookCommandHandler(
            IAggregateRepository<Storage> storageRepository,
            IOptions<StorageConfig> storageOptions,
            ICurrentUser currentUser)
        {
            _storageRepository = storageRepository;
            _currentUser = currentUser;
            _storageConfig = storageOptions.Value;
        }

        public async Task<Unit> Handle(BorrowBookCommand command, CancellationToken cancellationToken)
        {
            // TODO: New flow of borrowing should be changed to the following steps
            // 1. Get user from repo
            // 2. Get book from repo by its ISBN, not it directly
            // 3. Call Borrow on Book by passing user and dateTimePeriod for borrowing
            // 4. Call save on Book repository

            var spec = new StorageWithBooksAndLoansSpec(_storageConfig.DevelopStorageId);
            var storage = await _storageRepository.GetBySpecAsync(spec, cancellationToken);

            var dateTimePeriod = DateTimePeriod.Create(command.BorrowingStartDate, command.BorrowingEndDate);

            storage.BorrowBook(command.BookId, _currentUser.UserId, dateTimePeriod);

            await _storageRepository.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}