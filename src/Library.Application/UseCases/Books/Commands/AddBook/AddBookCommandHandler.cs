using System.Threading;
using System.Threading.Tasks;
using Library.Application.Auth;
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
            var libraryUser = await _libraryUserRepository.GetByIdAsync(_currentUser.UserId, cancellationToken);

            var book = Book.Create(command.Title, command.Author, command.Subject, command.Isbn);
            book.Register(libraryUser);

            await _bookRepository.AddAsync(book, cancellationToken);
            await _bookRepository.SaveChangesAsync(cancellationToken);

            return book.Id;
        }
    }
}