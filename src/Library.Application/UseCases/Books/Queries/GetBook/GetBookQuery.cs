﻿using Library.Application.UseCases.Books.ViewModels;
using MediatR;

namespace Library.Application.UseCases.Books.Queries.GetBook
{
    public class GetBookQuery : IRequest<BookViewModel>
    {
        public long BookId { get; }

        public GetBookQuery(long bookId) => BookId = bookId;
    }
}
