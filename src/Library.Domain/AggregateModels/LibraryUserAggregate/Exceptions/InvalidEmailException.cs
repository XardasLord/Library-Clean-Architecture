﻿using Library.Domain.Exceptions;

namespace Library.Domain.AggregateModels.LibraryUserAggregate.Exceptions
{
    public class InvalidEmailException : DomainException
    {
        public override string Code => "invalid_email_format";
        public string Email { get; }

        public InvalidEmailException(string email, string message) : base(message) 
            => Email = email;
    }
}