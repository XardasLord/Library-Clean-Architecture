using Library.Domain.AggregateModels.LibraryUserAggregate.Events;
using Library.Domain.Exceptions;
using Library.Domain.SeedWork;

namespace Library.Domain.AggregateModels.LibraryUserAggregate
{
    public class LibraryUser : AggregateRoot<long>
    {
        private string _login;
        private string _password;
        private string _firstName;
        private string _lastName;
        private Email _email;
        private bool _active;

        public string Login => _login;
        public string Password => _password;
        public string FirstName => _firstName;
        public string LastName => _lastName;
        public Email Email => _email;
        public bool Active => _active;

        private LibraryUser(string login, string password, string firstName, string lastName, string email)
        {
            ValidateInvariants(login, password, firstName, lastName, email);

            _login = login;
            _password = password; // TODO: Hash the plain password
            _firstName = firstName;
            _lastName = lastName;
            _email = new Email(email);
            _active = true;
        }

        private static void ValidateInvariants(string login, string password, string firstName, string lastName, string email)
        {
            if (string.IsNullOrWhiteSpace(login))
                throw new LibraryUserCreationException($"Parameter {nameof(login)} cannot be empty.");

            if (string.IsNullOrWhiteSpace(password))
                throw new LibraryUserCreationException($"Parameter {nameof(password)} cannot be empty.");

            if (string.IsNullOrWhiteSpace(firstName))
                throw new LibraryUserCreationException($"Parameter {nameof(firstName)} cannot be empty.");

            if (string.IsNullOrWhiteSpace(lastName))
                throw new LibraryUserCreationException($"Parameter {nameof(lastName)} cannot be empty.");

            if (string.IsNullOrWhiteSpace(email))
                throw new LibraryUserCreationException($"Parameter {nameof(email)} cannot be empty.");

            if (password.Length <= 5)
                throw new LibraryUserCreationException("Password has to be minimum 6 chars length.");
        }

        public static LibraryUser Create(string login, string password, string firstName, string lastName, string email)
        {
            var user = new LibraryUser(login, password, firstName, lastName, email);

            user.AddDomainEvent(new LibraryUserCreatedEvent(user));
            
            return user;
        }
    }
}
