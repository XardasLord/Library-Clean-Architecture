using System.Collections.Generic;
using Library.Application.UseCases.Books.Dtos;
using MediatR;

namespace Library.Application.UseCases.Books.Queries.GetAvailableBooks
{
    public class GetAvailableBooksQuery : IRequest<IReadOnlyCollection<BookDto>>
    {
    }
}
