using MediatR;

namespace Library.Application.UseCases.Storages.Commands.ReturnBook
{
    public class ReturnBookCommand : IRequest
    {
        public long BookId { get; }

        public ReturnBookCommand(long bookId) 
            => BookId = bookId;
    }
}
