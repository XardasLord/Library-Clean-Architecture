using MediatR;

namespace Library.Application.UseCases.Auth.Commands.Login
{
    public record LoginCommand(string Login, string Password) : IRequest<LoginCommandResponse>;
}
