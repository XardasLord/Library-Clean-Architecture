using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Library.Application.UseCases.Books.Exceptions;
using Library.Application.UseCases.Books.ViewModels;
using Library.Domain.AggregateModels.BookAggregate;
using Library.Domain.AggregateModels.BookAggregate.Specifications;
using Library.Domain.SharedKernel;
using MediatR;

namespace Library.Application.UseCases.Books.Queries.GetBook
{
    public class GetBookQueryHandler : IRequestHandler<GetBookQuery, BookViewModel>
    {
        private readonly IAggregateReadRepository<Book> _bookRepository;
        private readonly IMapper _mapper;

        public GetBookQueryHandler(
            IAggregateReadRepository<Book> bookRepository,
            IMapper mapper)
        {
            _bookRepository = bookRepository;
            _mapper = mapper;
        }

        public async Task<BookViewModel> Handle(GetBookQuery query, CancellationToken cancellationToken)
        {
            var book = await _bookRepository.GetBySpecAsync(new BookByIdSpec(query.BookId), cancellationToken)
                       ?? throw new BookNotFoundException(query.BookId);
            
            return _mapper.Map<BookViewModel>(book);
        }
    }
}