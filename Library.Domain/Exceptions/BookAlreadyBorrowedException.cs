using System;

namespace Library.Domain.Exceptions
{
    public class BookAlreadyBorrowedException : DomainException
    {
        protected override string Code => "book_is_already_borrowed";
        public DateTime BorrowedUntil { get; }

        public BookAlreadyBorrowedException(DateTime borrowedUntil) : base($"Book is already borrowed until {borrowedUntil.Date:yyyy-MM-dd}")
            => BorrowedUntil = borrowedUntil;
    }
}
