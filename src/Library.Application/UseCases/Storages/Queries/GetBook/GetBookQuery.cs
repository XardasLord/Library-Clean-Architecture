using Library.Application.UseCases.Storages.Dtos;
using MediatR;

namespace Library.Application.UseCases.Storages.Queries.GetBook
{
    public class GetBookQuery : IRequest<BookDto>
    {
        public long BookId { get; }

        public GetBookQuery(long bookId) => BookId = bookId;
    }
}
