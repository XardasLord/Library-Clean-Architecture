using MediatR;

namespace Library.Application.UseCases.LibraryUsers.Commands.RegisterLibraryUser
{
    public class RegisterLibraryUserCommand : IRequest
    {
        public string Login { get; set; }
        public string Password { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
    }
}
