using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Library.Application.UseCases.Books.Queries.GetAvailableBooks;
using Library.Application.UseCases.Books.ViewModels;
using Library.Infrastructure.Persistence.DbContexts;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Library.Infrastructure.Persistence.QueryHandlers
{
    public class GetAvailableBooksQueryHandler : IRequestHandler<GetAvailableBooksQuery, IReadOnlyCollection<BookViewModel>>
    {
        private readonly LibraryReadDbContext _context;
        private readonly IMapper _mapper;

        public GetAvailableBooksQueryHandler(LibraryReadDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
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
                .AsNoTracking()
                .ToListAsync(cancellationToken: cancellationToken);

            return books;
        }
    }
}