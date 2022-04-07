using System;
using System.Threading;
using System.Threading.Tasks;
using Library.Application.UseCases.Books.Exceptions;
using Library.Application.UseCases.LibraryUsers.Exceptions;
using Library.Domain.AggregateModels.BookAggregate;
using Library.Domain.AggregateModels.LibraryUserAggregate;
using Library.Domain.AggregateModels.LibraryUserAggregate.Specifications;
using Library.Domain.SharedKernel;
using MediatR;
using BookNotFoundException = Library.Application.UseCases.Books.Exceptions.BookNotFoundException;

namespace Library.Application.UseCases.Books.Commands.BorrowBook
{
    public class BorrowBookCommandHandler : IRequestHandler<BorrowBookCommand>
    {
        private readonly IAggregateRepository<LibraryUser> _libraryUserRepository;
        private readonly IAggregateRepository<Book> _bookRepository;
        private readonly ICurrentUser _currentUser;

        public BorrowBookCommandHandler(
            IAggregateRepository<LibraryUser> libraryUserRepository,
            IAggregateRepository<Book> bookRepository,
            ICurrentUser currentUser)
        {
            _libraryUserRepository = libraryUserRepository;
            _bookRepository = bookRepository;
            _currentUser = currentUser;
        }

        public async Task<Unit> Handle(BorrowBookCommand command, CancellationToken cancellationToken)
        {
            var spec = new LibraryUserWithActiveLoansSpec(_currentUser.UserId);
            var libraryUser = await _libraryUserRepository.GetBySpecAsync(spec, cancellationToken) 
                              ?? throw new LibraryUserNotFoundException(_currentUser.UserId);

            // TODO: Get book from repo by its ISBN, not it directly
            var book = await _bookRepository.GetByIdAsync(command.BookId, cancellationToken) 
                       ?? throw new BookNotFoundException(command.BookId);

            if (!book.InStock)
                throw new BookNotAvailableException(command.BookId);

            var dateTimePeriod = DateTimePeriod.Create(DateTime.UtcNow, command.BorrowingEndDate);

            libraryUser.BorrowBook(command.BookId, dateTimePeriod);

            await _libraryUserRepository.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}