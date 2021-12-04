using System;
using MediatR;

namespace Library.Application.UseCases.Books.Commands.BorrowBook
{
    public class BorrowBookCommand : IRequest
    {
        public long BookId { get; set; }
        public DateTime BorrowingEndDate { get; set; }
    }
}
