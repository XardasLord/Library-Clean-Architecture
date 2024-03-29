﻿using System;
using System.Collections.Generic;
using System.Net.Mail;
using Library.Domain.AggregateModels.LibraryUserAggregate.Exceptions;
using Library.Domain.Exceptions;
using Library.Domain.SeedWork;

namespace Library.Domain.AggregateModels.LibraryUserAggregate
{
    public record Email
    {
        public string Value { get; init; }

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

        public static implicit operator string(Email email) => email.Value;
        public static implicit operator Email(string email) => new(email);

        public override string ToString() => Value;
    }
}