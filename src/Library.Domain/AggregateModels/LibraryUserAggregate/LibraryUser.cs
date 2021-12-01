using Library.Domain.AggregateModels.LibraryUserAggregate.Events;
using Library.Domain.SeedWork;

namespace Library.Domain.AggregateModels.LibraryUserAggregate
{
    public class LibraryUser : Entity<long>, IAggregateRoot
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

        private LibraryUser(UserCredential credentials, Name name, string email)
        {
            _credentials = credentials;
            _firstName = name.FirstName;
            _lastName = name.LastName;
            _email = new Email(email);
            _isActive = true;
        }

        public static LibraryUser Create(UserCredential credentials, Name name, string email)
        {
            var user = new LibraryUser(credentials, name, email);

            user.AddDomainEvent(new LibraryUserCreatedEvent(user));
            
            return user;
        }

        public long Id { get; set; }
    }
}
