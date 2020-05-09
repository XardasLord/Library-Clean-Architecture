using Library.Application.UseCases.Storages.Dtos;
using MediatR;

namespace Library.Application.UseCases.Storages.Queries.GetBookByIsbn
{
    public class GetBookByIsbnQuery : IRequest<BookDto>
    {
        public string Isbn { get; }

        public GetBookByIsbnQuery(string isbn) 
            => Isbn = isbn;
    }
}
