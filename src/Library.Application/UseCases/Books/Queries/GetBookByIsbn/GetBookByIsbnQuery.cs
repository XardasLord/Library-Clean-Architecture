using Library.Application.UseCases.Books.Dtos;
using MediatR;

namespace Library.Application.UseCases.Books.Queries.GetBookByIsbn
{
    public class GetBookByIsbnQuery : IRequest<BookDto>
    {
        public string Isbn { get; }

        public GetBookByIsbnQuery(string isbn) 
            => Isbn = isbn;
    }
}
