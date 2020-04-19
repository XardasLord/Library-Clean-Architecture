using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Library.Application.UseCases.Books.Dtos;
using Library.Application.UseCases.Books.Exceptions;
using Library.Domain.AggregateModels.StorageAggregate;
using MediatR;

namespace Library.Application.UseCases.Books.Queries.GetBook
{
    public class GetBookQueryHandler : IRequestHandler<GetBookQuery, BookDto>
    {
        private readonly IBookRepository _bookRepository;
        private readonly IMapper _mapper;

        public GetBookQueryHandler(IBookRepository bookRepository, IMapper mapper)
        {
            _bookRepository = bookRepository;
            _mapper = mapper;
        }

        public async Task<BookDto> Handle(GetBookQuery query, CancellationToken cancellationToken)
        {
            var book = await _bookRepository.GetAsync(query.BookId)
                       ?? throw new BookNotFoundException(query.BookId);

            return _mapper.Map<BookDto>(book);
        }
    }
}