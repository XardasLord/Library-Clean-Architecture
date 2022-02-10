using System.Threading;
using System.Threading.Tasks;
using Library.Application.Auth;
using Library.Application.UseCases.Exceptions;
using Library.Domain.AggregateModels.LibraryUserAggregate;
using Library.Domain.AggregateModels.LibraryUserAggregate.Specifications;
using Library.Domain.SharedKernel;
using MediatR;

namespace Library.Application.UseCases.LibraryUsers.Commands.RegisterLibraryUser
{
    public class RegisterLibraryUserCommandHandler : IRequestHandler<RegisterLibraryUserCommand>
    {
        private readonly IAggregateRepository<LibraryUser> _repository;

        public RegisterLibraryUserCommandHandler(IAggregateRepository<LibraryUser> repository)
        {
            _repository = repository;
        }

        public async Task<Unit> Handle(RegisterLibraryUserCommand command, CancellationToken cancellationToken)
        {
            var spec = new LibraryUserByEmailSpec(command.Email);
            var existingLibraryUser = await _repository.GetBySpecAsync(spec, cancellationToken);
            
            if (existingLibraryUser is not null)
                throw new LibraryUserAlreadyExistsException(command.Email);

            var hashedPassword = PasswordManager.HashPassword(command.Password); // Should we do it here or is it a domain responsibility to hash password? I guess it's domain's
            var credentials = new UserCredential(command.Login, hashedPassword);
            var name = new Name(command.FirstName, command.LastName);
            var email = new Email(command.Email);

            var libraryUser = LibraryUser.Create(credentials, name, email);

            await _repository.AddAsync(libraryUser, cancellationToken);
            await _repository.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}