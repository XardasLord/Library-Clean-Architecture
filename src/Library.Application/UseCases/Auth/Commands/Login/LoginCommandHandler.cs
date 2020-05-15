using System.Threading;
using System.Threading.Tasks;
using Library.Application.Auth;
using Library.Application.UseCases.Exceptions;
using Library.Domain.AggregateModels.LibraryUserAggregate;
using MediatR;

namespace Library.Application.UseCases.Auth.Commands.Login
{
    public class LoginCommandHandler : IRequestHandler<LoginCommand, LoginCommandResponse>
    {
        private readonly ILibraryUserRepository _libraryUserRepository;
        private readonly IAuthService _authService;

        public LoginCommandHandler(ILibraryUserRepository libraryUserRepository, IAuthService authService)
        {
            _libraryUserRepository = libraryUserRepository;
            _authService = authService;
        }

        public async Task<LoginCommandResponse> Handle(LoginCommand query, CancellationToken cancellationToken)
        {
            var user = await _libraryUserRepository.GetAsync(query.Login)
                ?? throw new UserAuthenticationException("Invalid credentials.");

            if (!PasswordManager.VerifyHashedPassword(user.Credentials.Password, query.Password))
                throw new UserAuthenticationException("Invalid credentials.");

            if (!user.IsActive)
                throw new UserAuthenticationException("User is inactive.");

            var token = _authService.GenerateSecurityToken(user.Id, user.Email, $"{user.FirstName} {user.LastName}");

            return new LoginCommandResponse(token);
        }
    }
}