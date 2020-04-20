using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Library.Application.UseCases.Storages.Dtos;
using Library.Domain.AggregateModels.StorageAggregate;
using MediatR;

namespace Library.Application.UseCases.Storages.Queries.GetAvailableBooks
{
    public class GetAvailableBooksQueryHandler : IRequestHandler<GetAvailableBooksQuery, IReadOnlyCollection<BookDto>>
    {
        private readonly IBookRepository _bookRepository;
        private readonly IMapper _mapper;

        public GetAvailableBooksQueryHandler(IBookRepository bookRepository, IMapper mapper)
        {
            _bookRepository = bookRepository;
            _mapper = mapper;
        }

        public async Task<IReadOnlyCollection<BookDto>> Handle(GetAvailableBooksQuery query, CancellationToken cancellationToken)
        {
            var books = await _bookRepository.GetAllAvailableAsync();

            return _mapper.Map<IReadOnlyCollection<BookDto>>(books);
        }
    }
}