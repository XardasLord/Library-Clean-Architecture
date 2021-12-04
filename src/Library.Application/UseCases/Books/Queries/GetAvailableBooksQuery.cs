using System.Collections.Generic;
using Library.Application.UseCases.Books.ViewModels;
using MediatR;

namespace Library.Application.UseCases.Books.Queries
{
    public class GetAvailableBooksQuery : IRequest<IReadOnlyCollection<BookViewModel>>
    {
    }
}
