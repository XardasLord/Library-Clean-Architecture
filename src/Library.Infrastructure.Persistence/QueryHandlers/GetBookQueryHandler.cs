using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Library.Application.UseCases.Books.Queries;
using Library.Application.UseCases.Books.ViewModels;
using Library.Infrastructure.Persistence.DbContexts;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Library.Infrastructure.Persistence.QueryHandlers
{
    public class GetBookQueryHandler : IRequestHandler<GetBookQuery, BookViewModel>
    {
        private readonly LibraryReadDbContext _context;

        public GetBookQueryHandler(LibraryReadDbContext context)
        {
            _context = context;
        }

        public async Task<BookViewModel> Handle(GetBookQuery query, CancellationToken cancellationToken)
        {
            var book = await _context.BookReadModels
                .Where(x => x.Id == query.BookId)
                .Select(x => new BookViewModel
                {
                    Id = x.Id,
                    Author = x.Author,
                    Isbn = x.Isbn,
                    Subject = x.Subject,
                    Title = x.Title,
                    InStock = x.InStock
                })
                .AsNoTracking()
                .SingleOrDefaultAsync(cancellationToken: cancellationToken);

            return book;
        }
    }
}