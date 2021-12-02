using MediatR;

namespace Library.Application.UseCases.Books.Commands.ReturnBook
{
    public class ReturnBookCommand : IRequest
    {
        public long BookId { get; }

        public ReturnBookCommand(long bookId) 
            => BookId = bookId;
    }
}
