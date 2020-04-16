using System.Threading;
using System.Threading.Tasks;
using Library.Application.Auth;
using Library.Application.UseCases.Exceptions;
using Library.Domain.AggregateModels.LibraryUserAggregate;
using MediatR;

namespace Library.Application.UseCases.LibraryUsers.Commands.RegisterLibraryUser
{
    public class RegisterLibraryUserCommandHandler : IRequestHandler<RegisterLibraryUserCommand>
    {
        private readonly ILibraryUserRepository _libraryUserRepository;

        public RegisterLibraryUserCommandHandler(ILibraryUserRepository libraryUserRepository) 
            => _libraryUserRepository = libraryUserRepository;

        public async Task<Unit> Handle(RegisterLibraryUserCommand command, CancellationToken cancellationToken)
        {
            if (await _libraryUserRepository.ExistsAsync(command.Email))
                throw new LibraryUserAlreadyExistsException(command.Email);

            var hashedPassword = PasswordManager.HashPassword(command.Password);

            var libraryUser = LibraryUser.Create(command.Login, hashedPassword, command.FirstName, command.LastName, command.Email);

            await _libraryUserRepository.AddAsync(libraryUser);
            await _libraryUserRepository.SaveChangesAsync();

            return Unit.Value;
        }
    }
}