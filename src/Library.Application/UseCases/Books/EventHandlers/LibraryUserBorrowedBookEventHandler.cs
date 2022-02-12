using System.Threading;
using System.Threading.Tasks;
using Library.Application.UseCases.Books.Exceptions;
using Library.Domain.AggregateModels.BookAggregate;
using Library.Domain.AggregateModels.LibraryUserAggregate.Events;
using Library.Domain.SharedKernel;
using MediatR;

namespace Library.Application.UseCases.Books.EventHandlers
{
    public class LibraryUserBorrowedBookEventHandler : INotificationHandler<LibraryUserBorrowedBookEvent>
    {
        private readonly IAggregateRepository<Book> _bookRepository;

        public LibraryUserBorrowedBookEventHandler(IAggregateRepository<Book> bookRepository)
        {
            _bookRepository = bookRepository;
        }
        
        public async Task Handle(LibraryUserBorrowedBookEvent @event, CancellationToken cancellationToken)
        {
            var book = await _bookRepository.GetByIdAsync(@event.BookId, cancellationToken) 
                       ?? throw new BookNotFoundException(@event.BookId);

            book.SetAsNotAvailable(@event.LibraryUserId, @event.BorrowPeriod);

            await _bookRepository.SaveChangesAsync(cancellationToken);
        }
    }
}