﻿using System.Collections.Generic;
using System.Linq;
using Library.Domain.AggregateModels.BookAggregate;
using Library.Domain.AggregateModels.LibraryUserAggregate.Events;
using Library.Domain.AggregateModels.LibraryUserAggregate.Exceptions;
using Library.Domain.SeedWork;
using Library.Domain.SharedKernel;

namespace Library.Domain.AggregateModels.LibraryUserAggregate
{
    public class LibraryUser : Entity<long>, IAggregateRoot
    {
        private UserCredential _credentials;
        private string _firstName;
        private string _lastName;
        private Email _email;
        private bool _isActive;
        internal List<Loan> _activeLoans;

        public UserCredential Credentials => _credentials;
        public string FirstName => _firstName;
        public string LastName => _lastName;
        public Email Email => _email;
        public bool IsActive => _isActive;
        public IReadOnlyCollection<Loan> ActiveLoans => _activeLoans.Where(x => x.IsActive).ToList();
        
        internal LibraryUser()
        {
            _activeLoans = new List<Loan>();
        }

        private LibraryUser(UserCredential credentials, Name name, Email email)
        {
            _credentials = credentials;
            _firstName = name.FirstName;
            _lastName = name.LastName;
            _email = email;
            _isActive = true;
        }

        public static LibraryUser Create(UserCredential credentials, Name name, Email email)
        {
            var user = new LibraryUser(credentials, name, email);

            user.AddDomainEvent(new LibraryUserCreatedEvent(user));
            
            return user;
        }

        public void BorrowBook(long bookId, DateTimePeriod borrowPeriod)
        {
            if (ActiveLoans.Count == 3)
                throw new LibraryUserMaximumBooksBorrowedExceededException();
            
            // book.Borrow(this, borrowPeriod); // TODO: This should be handled in a separate transaction - in a handler of the event fired below
            
            _activeLoans.Add(Loan.Create(bookId, Id, borrowPeriod));

            AddDomainEvent(new LibraryUserBorrowedBookEvent(Id, bookId, borrowPeriod));
        }

        public void ReturnBook(Book book)
        {
            var bookLoanEntry = ActiveLoans.FirstOrDefault(x => x.BookId == book.Id);
            
            if (bookLoanEntry is null)
                throw new LibraryUserDoesNotHaveBookBorrowed(book.Id);

            book.Return(this); // TODO: This should be handled in a separate transaction - in a handler of the event fired below
            bookLoanEntry.Finish();
            
            AddDomainEvent(new LibraryUserReturnedBookEvent(Id, book.Id));
        }
    }
}
