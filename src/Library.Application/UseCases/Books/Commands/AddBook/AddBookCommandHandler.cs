using System.Threading;
using System.Threading.Tasks;
using Library.Application.UseCases.LibraryUsers.Exceptions;
using Library.Domain.AggregateModels.BookAggregate;
using Library.Domain.AggregateModels.LibraryUserAggregate;
using Library.Domain.SharedKernel;
using MediatR;

namespace Library.Application.UseCases.Books.Commands.AddBook
{
    public class AddBookCommandHandler : IRequestHandler<AddBookCommand, long>
    {
        private readonly IAggregateRepository<LibraryUser> _libraryUserRepository;
        private readonly IAggregateRepository<Book> _bookRepository;
        private readonly ICurrentUser _currentUser;

        public AddBookCommandHandler(
            IAggregateRepository<LibraryUser> libraryUserRepository,
            IAggregateRepository<Book> bookRepository,
            ICurrentUser currentUser)
        {
            _libraryUserRepository = libraryUserRepository;
            _bookRepository = bookRepository;
            _currentUser = currentUser;
        }

        public async Task<long> Handle(AddBookCommand command, CancellationToken cancellationToken)
        {
            var libraryUser = await _libraryUserRepository.GetByIdAsync(_currentUser.UserId, cancellationToken)
                ?? throw new LibraryUserNotFoundException(_currentUser.UserId);

            var book = Book.Register(command.Title, command.Author, command.Subject, command.Isbn, libraryUser.Id);

            await _bookRepository.AddAsync(book, cancellationToken);

            return book.Id;
        }
    }
}