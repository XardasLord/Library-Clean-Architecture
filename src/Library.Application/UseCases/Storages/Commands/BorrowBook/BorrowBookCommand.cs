using System;
using MediatR;

namespace Library.Application.UseCases.Storages.Commands.BorrowBook
{
    public class BorrowBookCommand : IRequest
    {
        public long BookId { get; set; }
        public long UserId { get; set; }
        public DateTime BorrowingEndDate { get; set; }
    }
}
