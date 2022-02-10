using Library.Application.UseCases.Books.ViewModels;
using MediatR;

namespace Library.Application.UseCases.Books.Queries
{
    public class GetBookByIsbnQuery : IRequest<BookViewModel>
    {
        public string Isbn { get; }

        public GetBookByIsbnQuery(string isbn) 
            => Isbn = isbn;
    }
}
