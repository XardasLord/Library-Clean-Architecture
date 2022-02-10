using MediatR;

namespace Library.Application.UseCases.LibraryUsers.Commands.RegisterLibraryUser
{
    public record RegisterLibraryUserCommand(string Login, string Password, string FirstName, string LastName, string Email) : IRequest;
}