using MediatR;

namespace Library.Application.UseCases.Books.Commands.ReturnBook
{
    public record ReturnBookCommand(long BookId) : IRequest;
}
