using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Library.Application.UseCases.Books.ViewModels;
using Library.Domain.AggregateModels.BookAggregate;
using Library.Domain.AggregateModels.BookAggregate.Specifications;
using Library.Domain.SharedKernel;
using MediatR;

namespace Library.Application.UseCases.Books.Queries.GetBookByIsbn
{
    public class GetBookByIsbnQueryHandler : IRequestHandler<GetBookByIsbnQuery, BookViewModel>
    {
        private readonly IAggregateReadRepository<Book> _bookRepository;
        private readonly IMapper _mapper;

        public GetBookByIsbnQueryHandler(
            IAggregateReadRepository<Book> bookRepository,
            IMapper mapper)
        {
            _bookRepository = bookRepository;
            _mapper = mapper;
        }

        public async Task<BookViewModel> Handle(GetBookByIsbnQuery query, CancellationToken cancellationToken)
        {
            var isbn = new Isbn(query.Isbn);
            var book = await _bookRepository.GetBySpecAsync(new BookByIsbnSpec(isbn), cancellationToken);

            return book is null 
                ? null 
                : _mapper.Map<BookViewModel>(book);
        }
    }
}