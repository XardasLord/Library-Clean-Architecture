using System.Threading;
using System.Threading.Tasks;
using Library.Application.Auth;
using Library.Application.UseCases.Exceptions;
using Library.Domain.AggregateModels.LibraryUserAggregate;
using Library.Domain.AggregateModels.LibraryUserAggregate.Specifications;
using Library.Domain.SharedKernel;
using MediatR;

namespace Library.Application.UseCases.Auth.Commands.Login
{
    public class LoginCommandHandler : IRequestHandler<LoginCommand, LoginCommandResponse>
    {
        private readonly IAggregateRepository<LibraryUser> _repository;
        private readonly IAuthService _authService;

        public LoginCommandHandler(IAggregateRepository<LibraryUser> repository, IAuthService authService)
        {
            _repository = repository;
            _authService = authService;
        }

        public async Task<LoginCommandResponse> Handle(LoginCommand query, CancellationToken cancellationToken)
        {
            var spec = new LibraryUserByLoginSpec(query.Login);
            var user = await _repository.GetBySpecAsync(spec, cancellationToken)
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