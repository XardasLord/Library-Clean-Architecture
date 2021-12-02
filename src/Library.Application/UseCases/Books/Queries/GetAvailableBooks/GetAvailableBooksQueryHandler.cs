using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Library.Application.UseCases.Books.ViewModels;
using Library.Domain.AggregateModels.BookAggregate;
using Library.Domain.AggregateModels.BookAggregate.Specifications;
using Library.Domain.SharedKernel;
using MediatR;

namespace Library.Application.UseCases.Books.Queries.GetAvailableBooks
{
    public class GetAvailableBooksQueryHandler : IRequestHandler<GetAvailableBooksQuery, IReadOnlyCollection<BookViewModel>>
    {
        private readonly IAggregateReadRepository<Book> _bookRepository;
        private readonly IMapper _mapper;

        public GetAvailableBooksQueryHandler(
            IAggregateReadRepository<Book> bookRepository,
            IMapper mapper)
        {
            _bookRepository = bookRepository;
            _mapper = mapper;
        }

        public async Task<IReadOnlyCollection<BookViewModel>> Handle(GetAvailableBooksQuery query, CancellationToken cancellationToken)
        {
            var books = await _bookRepository.ListAsync(new AvailableBooksSpec(), cancellationToken);

            return _mapper.Map<IReadOnlyCollection<BookViewModel>>(books);
        }
    }
}