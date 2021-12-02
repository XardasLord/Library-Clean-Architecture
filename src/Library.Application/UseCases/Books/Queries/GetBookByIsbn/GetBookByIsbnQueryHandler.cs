using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Library.Application.UseCases.Books.Dtos;
using Library.Domain.AggregateModels.BookAggregate;
using Library.Domain.AggregateModels.BookAggregate.Specifications;
using Library.Domain.SharedKernel;
using MediatR;

namespace Library.Application.UseCases.Books.Queries.GetBookByIsbn
{
    public class GetBookByIsbnQueryHandler : IRequestHandler<GetBookByIsbnQuery, BookDto>
    {
        private readonly IAggregateRepository<Book> _bookRepository;
        private readonly IMapper _mapper;

        public GetBookByIsbnQueryHandler(
            IAggregateRepository<Book> bookRepository,
            IMapper mapper)
        {
            _bookRepository = bookRepository;
            _mapper = mapper;
        }

        public async Task<BookDto> Handle(GetBookByIsbnQuery query, CancellationToken cancellationToken)
        {
            var isbn = new Isbn(query.Isbn);
            var book = await _bookRepository.GetBySpecAsync(new BookByIsbnSpec(isbn), cancellationToken);

            return book is null 
                ? null 
                : _mapper.Map<BookDto>(book);
        }
    }
}