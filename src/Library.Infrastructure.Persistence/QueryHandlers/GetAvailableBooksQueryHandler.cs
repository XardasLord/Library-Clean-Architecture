using System.Collections.Generic;
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
    public class GetAvailableBooksQueryHandler : IRequestHandler<GetAvailableBooksQuery, IReadOnlyCollection<BookViewModel>>
    {
        private readonly ReadDbContext _context;

        public GetAvailableBooksQueryHandler(ReadDbContext context)
        {
            _context = context;
        }

        public async Task<IReadOnlyCollection<BookViewModel>> Handle(GetAvailableBooksQuery query, CancellationToken cancellationToken)
        {
            var books = await _context.BookReadModels
                .Where(x => x.InStock)
                .Select(x => new BookViewModel
                {
                    Id = x.Id,
                    Author = x.Author,
                    Isbn = x.Isbn,
                    Subject = x.Subject,
                    Title = x.Title,
                    InStock = x.InStock
                })
                .ToListAsync(cancellationToken: cancellationToken);

            return books;
        }
    }
}