using System.Threading;
using System.Threading.Tasks;
using Library.Application.Auth;
using Library.Application.UseCases.Books.Exceptions;
using Library.Domain.AggregateModels.BookAggregate;
using Library.Domain.AggregateModels.LibraryUserAggregate;
using Library.Domain.SharedKernel;
using MediatR;

namespace Library.Application.UseCases.Books.Commands.ReturnBook
{
    public class ReturnBookCommandHandler : IRequestHandler<ReturnBookCommand>
    {
        private readonly IAggregateRepository<LibraryUser> _libraryUserRepository;
        private readonly IAggregateRepository<Book> _bookRepository;
        private readonly ICurrentUser _currentUser;

        public ReturnBookCommandHandler(
            IAggregateRepository<LibraryUser> libraryUserRepository,
            IAggregateRepository<Book> bookRepository,
            ICurrentUser currentUser)
        {
            _libraryUserRepository = libraryUserRepository;
            _bookRepository = bookRepository;
            _currentUser = currentUser;
        }

        public async Task<Unit> Handle(ReturnBookCommand command, CancellationToken cancellationToken)
        {
            var libraryUser = await _libraryUserRepository.GetByIdAsync(_currentUser.UserId, cancellationToken);
            
            var book = await _bookRepository.GetByIdAsync(command.BookId, cancellationToken)
                       ?? throw new BookNotFoundException(command.BookId);

            book.Return(libraryUser);

            await _bookRepository.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}