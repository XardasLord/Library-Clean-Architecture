using MediatR;

namespace Library.Application.UseCases.Books.Commands.AddBook
{
    public record AddBookCommand(string Title, string Author, string Subject, string Isbn) : IRequest<long>;
}
