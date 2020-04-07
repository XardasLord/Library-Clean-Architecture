using System.Threading;
using System.Threading.Tasks;
using Library.Domain.AggregateModels.BookAggregate;
using MediatR;

namespace Library.Application.UseCases.Books.Commands
{
    public class AddBookCommandHandler : IRequestHandler<AddBookCommand, long>
    {
        private readonly IBookRepository _bookRepository;

        public AddBookCommandHandler(IBookRepository bookRepository) 
            => _bookRepository = bookRepository;

        public async Task<long> Handle(AddBookCommand command, CancellationToken cancellationToken)
        {
            var book = Book.Create(command.Title, command.Author, command.Subject, command.Isbn);
            
            await _bookRepository.AddAsync(book);
            await _bookRepository.SaveChangesAsync();

            return book.Id;
        }
    }
}