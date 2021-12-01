using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Library.Application.Configurations;
using Library.Application.UseCases.Storages.Dtos;
using Library.Application.UseCases.Storages.Exceptions;
using Library.Domain.AggregateModels.StorageAggregate;
using Library.Domain.AggregateModels.StorageAggregate.Specifications;
using Library.Domain.SharedKernel;
using MediatR;
using Microsoft.Extensions.Options;

namespace Library.Application.UseCases.Storages.Queries.GetBook
{
    public class GetBookQueryHandler : IRequestHandler<GetBookQuery, BookDto>
    {
        private readonly IAggregateRepository<Storage> _storageRepository;
        private readonly StorageConfig _storageConfig;
        private readonly IMapper _mapper;

        public GetBookQueryHandler(
            IAggregateRepository<Storage> storageRepository,
            IOptions<StorageConfig> storageOptions,
            IMapper mapper)
        {
            _storageRepository = storageRepository;
            _storageConfig = storageOptions.Value;
            _mapper = mapper;
        }

        public async Task<BookDto> Handle(GetBookQuery query, CancellationToken cancellationToken)
        {
            var spec = new StorageWithBooksAndLoansSpec(_storageConfig.DevelopStorageId);
            var storage = await _storageRepository.GetBySpecAsync(spec, cancellationToken);

            var book = storage.Books.SingleOrDefault(x => x.Id == query.BookId)
                       ?? throw new BookNotFoundException(query.BookId);

            return _mapper.Map<BookDto>(book);
        }
    }
}