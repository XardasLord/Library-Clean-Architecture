using System;
using System.Collections.Generic;
using System.Net.Mail;
using Library.Domain.Exceptions;
using Library.Domain.SeedWork;

namespace Library.Domain.AggregateModels.LibraryUserAggregate
{
    public class Email :ValueObject
    {
        public string Value { get; }

        private Email() { }

        public Email(string email)
        {
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

        public override string ToString() => Value;
    }
}