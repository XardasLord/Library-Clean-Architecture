using Library.Domain.AggregateModels.LibraryUserAggregate.Events;
using Library.Domain.Exceptions;
using Library.Domain.SeedWork;

namespace Library.Domain.AggregateModels.LibraryUserAggregate
{
    public class LibraryUser : AggregateRoot<long>
    {
        private UserCredential _credentials;
        private string _firstName;
        private string _lastName;
        private Email _email;
        private bool _isActive;

        public UserCredential Credentials => _credentials;
        public string FirstName => _firstName;
        public string LastName => _lastName;
        public Email Email => _email;
        public bool IsActive => _isActive;

        private LibraryUser() { }

        private LibraryUser(UserCredential credentials, string firstName, string lastName, string email)
        {
            ValidateInputs(firstName, lastName, email);

            _credentials = credentials;
            _firstName = firstName;
            _lastName = lastName;
            _email = new Email(email);
            _isActive = true;
        }

        private static void ValidateInputs(string firstName, string lastName, string email)
        {
            if (string.IsNullOrWhiteSpace(firstName))
                throw new LibraryUserCreationException($"Parameter {nameof(firstName)} cannot be empty.");

            if (string.IsNullOrWhiteSpace(lastName))
                throw new LibraryUserCreationException($"Parameter {nameof(lastName)} cannot be empty.");

            if (string.IsNullOrWhiteSpace(email))
                throw new LibraryUserCreationException($"Parameter {nameof(email)} cannot be empty.");
        }

        public static LibraryUser Create(UserCredential credentials, string firstName, string lastName, string email)
        {
            var user = new LibraryUser(credentials, firstName, lastName, email);

            user.AddDomainEvent(new LibraryUserCreatedEvent(user));
            
            return user;
        }
    }
}
