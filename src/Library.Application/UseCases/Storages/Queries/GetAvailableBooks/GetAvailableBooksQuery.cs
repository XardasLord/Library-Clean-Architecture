using System.Collections.Generic;
using Library.Application.UseCases.Storages.Dtos;
using MediatR;

namespace Library.Application.UseCases.Storages.Queries.GetAvailableBooks
{
    public class GetAvailableBooksQuery : IRequest<IReadOnlyCollection<BookDto>>
    {
    }
}
