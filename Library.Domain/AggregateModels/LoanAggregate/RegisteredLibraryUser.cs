using Library.Domain.SeedWork;

namespace Library.Domain.AggregateModels.LoanAggregate
{
    public class RegisteredLibraryUser : Entity<long>
    {
        private string _firstName;
        private string _lastName;
        private string _email;

        public string FirstName => _firstName;
        public string LastName => _lastName;
        public string Email => _email;
    }
}
