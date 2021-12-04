using System;
using System.Threading;
using System.Threading.Tasks;
using Library.Application.UseCases.Books.Exceptions;
using Library.Domain.AggregateModels.BookAggregate;
using Library.Domain.AggregateModels.BookAggregate.Specifications;
using Library.Domain.AggregateModels.LibraryUserAggregate;
using Library.Domain.SharedKernel;
using MediatR;

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
            var libraryUser = await _libraryUserRepository.GetByIdAsync(_currentUser.UserId, cancellationToken);
            
            // TODO: Get book from repo by its ISBN, not it directly
            var spec = new BookByIdSpec(command.BookId);
            var book = await _bookRepository.GetBySpecAsync(spec, cancellationToken)
                       ?? throw new BookNotFoundException(command.BookId);
            
            var dateTimePeriod = DateTimePeriod.Create(DateTime.UtcNow, command.BorrowingEndDate);
            book.Borrow(libraryUser, dateTimePeriod);
            
            await _bookRepository.SaveChangesAsync(cancellationToken);
            
            return Unit.Value;
        }
    }
}