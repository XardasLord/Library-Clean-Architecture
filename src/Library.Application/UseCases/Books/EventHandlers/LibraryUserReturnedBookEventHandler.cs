using System.Threading;
using System.Threading.Tasks;
using Library.Application.UseCases.Books.Exceptions;
using Library.Domain.AggregateModels.BookAggregate;
using Library.Domain.AggregateModels.LibraryUserAggregate.Events;
using Library.Domain.SharedKernel;
using MediatR;

namespace Library.Application.UseCases.Books.EventHandlers
{
    public class LibraryUserReturnedBookEventHandler : INotificationHandler<LibraryUserReturnedBookEvent>
    {
        private readonly IAggregateRepository<Book> _bookRepository;

        public LibraryUserReturnedBookEventHandler(IAggregateRepository<Book> bookRepository)
        {
            _bookRepository = bookRepository;
        }
        
        public async Task Handle(LibraryUserReturnedBookEvent @event, CancellationToken cancellationToken)
        {
            var book = await _bookRepository.GetByIdAsync(@event.BookId, cancellationToken)
                    ?? throw new BookNotFoundException(@event.BookId);

            book.SetAsAvailable(@event.LibraryUserId);

            await _bookRepository.SaveChangesAsync(cancellationToken);
        }
    }
}