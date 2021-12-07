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
    public class GetBookByIsbnQueryHandler : IRequestHandler<GetBookByIsbnQuery, BookViewModel>
    {
        private readonly ReadDbContext _context;

        public GetBookByIsbnQueryHandler(ReadDbContext context)
        {
            _context = context;
        }

        public async Task<BookViewModel> Handle(GetBookByIsbnQuery query, CancellationToken cancellationToken)
        {
            var book = await _context.BookReadModels
                .Where(x => x.Isbn == query.Isbn)
                .Select(x => new BookViewModel
                {
                    Id = x.Id,
                    Author = x.Author,
                    Isbn = x.Isbn,
                    Subject = x.Subject,
                    Title = x.Title,
                    InStock = x.InStock
                })
                .SingleOrDefaultAsync(cancellationToken: cancellationToken);

            return book;
        }
    }
}