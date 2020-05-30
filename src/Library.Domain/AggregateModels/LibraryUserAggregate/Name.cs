using System.Collections.Generic;
using Library.Domain.Exceptions;
using Library.Domain.SeedWork;

namespace Library.Domain.AggregateModels.LibraryUserAggregate
{
    public class Name : ValueObject
    {
        public string FirstName { get; }
        public string LastName { get; }

        public Name() { }

        public Name(string firstName, string lastName)
        {
            if (string.IsNullOrWhiteSpace(firstName))
                throw new LibraryUserCreationException($"Parameter {nameof(firstName)} cannot be empty.");

            if (string.IsNullOrWhiteSpace(lastName))
                throw new LibraryUserCreationException($"Parameter {nameof(lastName)} cannot be empty.");

            FirstName = firstName;
            LastName = lastName;
        }

        protected override IEnumerable<object> GetAtomicValues()
        {
            yield return FirstName;
            yield return LastName;
        }

        public static implicit operator string(Name name) => name.ToString();

        public override string ToString() => $"{FirstName} {LastName}";
    }
}
