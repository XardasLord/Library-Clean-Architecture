using System;
using MediatR;

namespace Library.Application.UseCases.Storages.Commands.BorrowBook
{
    public class BorrowBookCommand : IRequest
    {
        public long BookId { get; set; }
        public DateTime BorrowingStartDate { get; set; }
        public DateTime BorrowingEndDate { get; set; }
    }
}
