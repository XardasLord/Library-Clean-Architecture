using Library.Domain.SeedWork;

namespace Library.Domain.AggregateModels.LibraryUserAggregate
{
    public class LibraryUser : AggregateRoot<long>
    {
        private string _firstName;
        private string _lastName;
        private string _email;
        private bool _active;

        public string FirstName => _firstName;
        public string LastName => _lastName;
        public string Email => _email;
        public bool Active => _active;

        private LibraryUser(string firstName, string lastName, string email)
        {
            _firstName = firstName;
            _lastName = lastName;
            _email = email;
            _active = true;
        }

        public static LibraryUser Create(string firstName, string lastName, string email)
        {
            var user = new LibraryUser(firstName, lastName, email);
            
            return user;
        }
    }
}
