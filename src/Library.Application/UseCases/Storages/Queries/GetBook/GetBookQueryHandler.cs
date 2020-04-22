using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Library.Application.Configurations;
using Library.Application.UseCases.Storages.Dtos;
using Library.Application.UseCases.Storages.Exceptions;
using Library.Domain.AggregateModels.StorageAggregate;
using MediatR;
using Microsoft.Extensions.Options;

namespace Library.Application.UseCases.Storages.Queries.GetBook
{
    public class GetBookQueryHandler : IRequestHandler<GetBookQuery, BookDto>
    {
        private readonly IStorageRepository _storageRepository;
        private readonly StorageConfig _storageConfig;
        private readonly IMapper _mapper;

        public GetBookQueryHandler(IStorageRepository storageRepository, IOptions<StorageConfig> storageOptions, IMapper mapper)
        {
            _storageRepository = storageRepository;
            _storageConfig = storageOptions.Value;
            _mapper = mapper;
        }

        public async Task<BookDto> Handle(GetBookQuery query, CancellationToken cancellationToken)
        {
            var storage = await _storageRepository.GetAsync(_storageConfig.DevelopStorageId);

            var book = storage.Books.SingleOrDefault(x => x.Id == query.BookId)
                       ?? throw new BookNotFoundException(query.BookId);

            return _mapper.Map<BookDto>(book);
        }
    }
}