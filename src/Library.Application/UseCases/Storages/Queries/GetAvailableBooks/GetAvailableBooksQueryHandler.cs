using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Library.Application.Configurations;
using Library.Application.UseCases.Storages.Dtos;
using Library.Domain.AggregateModels.StorageAggregate;
using Library.Domain.AggregateModels.StorageAggregate.Specifications;
using Library.Domain.SharedKernel;
using MediatR;
using Microsoft.Extensions.Options;

namespace Library.Application.UseCases.Storages.Queries.GetAvailableBooks
{
    public class GetAvailableBooksQueryHandler : IRequestHandler<GetAvailableBooksQuery, IReadOnlyCollection<BookDto>>
    {
        private readonly IAggregateRepository<Storage> _storageRepository;
        private readonly StorageConfig _storageConfig;
        private readonly IMapper _mapper;

        public GetAvailableBooksQueryHandler(IAggregateRepository<Storage> storageRepository, IOptions<StorageConfig> storageConfig, IMapper mapper)
        {
            _storageRepository = storageRepository;
            _storageConfig = storageConfig.Value;
            _mapper = mapper;
        }

        public async Task<IReadOnlyCollection<BookDto>> Handle(GetAvailableBooksQuery query, CancellationToken cancellationToken)
        {
            var spec = new StorageWithBooksAndLoansSpec(_storageConfig.DevelopStorageId);
            var storage = await _storageRepository.GetBySpecAsync(spec, cancellationToken);

            return _mapper.Map<IReadOnlyCollection<BookDto>>(storage.AvailableBooks);
        }
    }
}