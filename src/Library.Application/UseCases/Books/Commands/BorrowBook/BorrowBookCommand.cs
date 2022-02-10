using System;
using MediatR;

namespace Library.Application.UseCases.Books.Commands.BorrowBook
{
    public record BorrowBookCommand(long BookId, DateTime BorrowingEndDate) : IRequest;

}
