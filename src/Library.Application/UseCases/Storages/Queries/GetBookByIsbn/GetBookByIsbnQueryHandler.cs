using System.Linq;
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

namespace Library.Application.UseCases.Storages.Queries.GetBookByIsbn
{
    public class GetBookByIsbnQueryHandler : IRequestHandler<GetBookByIsbnQuery, BookDto>
    {
        private readonly IAggregateRepository<Storage> _storageRepository;
        private readonly StorageConfig _storageConfig;
        private readonly IMapper _mapper;

        public GetBookByIsbnQueryHandler(
            IAggregateRepository<Storage> storageRepository,
            IOptions<StorageConfig> storageOptions,
            IMapper mapper)
        {
            _storageRepository = storageRepository;
            _storageConfig = storageOptions.Value;
            _mapper = mapper;
        }

        public async Task<BookDto> Handle(GetBookByIsbnQuery query, CancellationToken cancellationToken)
        {
            var spec = new StorageWithBooksAndLoansSpec(_storageConfig.DevelopStorageId);
            var storage = await _storageRepository.GetBySpecAsync(spec, cancellationToken);

            var isbn = new Isbn(query.Isbn);
            var book = storage.Books.FirstOrDefault(x => x.BookInformation.Isbn.Equals(isbn));

            return book is null ? null : _mapper.Map<BookDto>(book);
        }
    }
}