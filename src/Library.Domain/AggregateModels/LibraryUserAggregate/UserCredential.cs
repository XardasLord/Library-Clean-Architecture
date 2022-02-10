using Library.Domain.AggregateModels.LibraryUserAggregate.Exceptions;

namespace Library.Domain.AggregateModels.LibraryUserAggregate
{
    public record UserCredential
    {
        public string Login { get; }
        public string Password { get; }

        private UserCredential()
        {
        }

        public UserCredential(string login, string password)
        {
            // TODO: Implement Guard clause
            if (string.IsNullOrWhiteSpace(login))
                throw new InvalidCredentialsException($"Parameter {nameof(login)} cannot be empty.");

            if (string.IsNullOrWhiteSpace(password))
                throw new InvalidCredentialsException($"Parameter {nameof(password)} cannot be empty.");

            if (password.Length <= 5)
                throw new InvalidCredentialsException("Password has to be at least 6 characters length.");

            Login = login;
            Password = password;
        }
    }
}
