using System;
using System.Collections.Generic;
using System.Net.Mail;
using Library.Domain.AggregateModels.LibraryUserAggregate.Exceptions;
using Library.Domain.Exceptions;
using Library.Domain.SeedWork;

namespace Library.Domain.AggregateModels.LibraryUserAggregate
{
    public class Email : ValueObject
    {
        public string Value { get; }

        private Email() { }

        public Email(string email)
        {
            // TODO: Implement Guard clause
            try
            {
                var emailAddress = new MailAddress(email);
                
                Value = emailAddress.Address;
            }
            catch (Exception ex)
            {
                throw new InvalidEmailException(email, ex.Message);
            }
        }

        protected override IEnumerable<object> GetAtomicValues()
        {
            yield return Value;
        }

        public static implicit operator string(Email email) => email.Value;
        public static implicit operator Email(string email) => new(email);

        public override string ToString() => Value;
    }
}