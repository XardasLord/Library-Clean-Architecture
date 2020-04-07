using MediatR;

namespace Library.Application.UseCases.Books.Queries.GetBook
{
    public class GetBookQuery : IRequest<BookDto>
    {
        public long BookId { get; }

        public GetBookQuery(long bookId) => BookId = bookId;
    }
}
