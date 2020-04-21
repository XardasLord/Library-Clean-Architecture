using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Library.Application.Configurations;
using Library.Application.UseCases.Storages.Dtos;
using Library.Domain.AggregateModels.StorageAggregate;
using MediatR;
using Microsoft.Extensions.Options;

namespace Library.Application.UseCases.Storages.Queries.GetAvailableBooks
{
    public class GetAvailableBooksQueryHandler : IRequestHandler<GetAvailableBooksQuery, IReadOnlyCollection<BookDto>>
    {
        private readonly IStorageRepository _storageRepository;
        private readonly StorageConfig _storageConfig;
        private readonly IMapper _mapper;

        public GetAvailableBooksQueryHandler(IStorageRepository storageRepository, IOptions<StorageConfig> storageConfig, IMapper mapper)
        {
            _storageRepository = storageRepository;
            _storageConfig = storageConfig.Value;
            _mapper = mapper;
        }

        public async Task<IReadOnlyCollection<BookDto>> Handle(GetAvailableBooksQuery query, CancellationToken cancellationToken)
        {
            var storage = await _storageRepository.GetAsync(_storageConfig.DevelopStorageId);

            return _mapper.Map<IReadOnlyCollection<BookDto>>(storage.AvailableBooks);
        }
    }
}