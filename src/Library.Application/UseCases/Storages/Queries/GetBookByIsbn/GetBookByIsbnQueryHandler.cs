using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Library.Application.Configurations;
using Library.Application.UseCases.Storages.Dtos;
using Library.Domain.AggregateModels.StorageAggregate;
using MediatR;
using Microsoft.Extensions.Options;

namespace Library.Application.UseCases.Storages.Queries.GetBookByIsbn
{
    public class GetBookByIsbnQueryHandler : IRequestHandler<GetBookByIsbnQuery, BookDto>
    {
        private readonly IStorageRepository _storageRepository;
        private readonly StorageConfig _storageConfig;
        private readonly IMapper _mapper;

        public GetBookByIsbnQueryHandler(IStorageRepository storageRepository, IOptions<StorageConfig> storageOptions, IMapper mapper)
        {
            _storageRepository = storageRepository;
            _storageConfig = storageOptions.Value;
            _mapper = mapper;
        }

        public async Task<BookDto> Handle(GetBookByIsbnQuery query, CancellationToken cancellationToken)
        {
            var storage = await _storageRepository.GetAsync(_storageConfig.DevelopStorageId);

            var isbn = new Isbn(query.Isbn);
            var book = storage.Books.FirstOrDefault(x => x.BookInformation.Isbn.Equals(isbn));

            return book is null ? null : _mapper.Map<BookDto>(book);
        }
    }
}