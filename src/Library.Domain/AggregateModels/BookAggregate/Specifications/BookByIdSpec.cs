﻿using Ardalis.Specification;

namespace Library.Domain.AggregateModels.BookAggregate.Specifications
{
    public sealed class BookByIdSpec : Specification<Book>, ISingleResultSpecification
    {
        public BookByIdSpec(long bookId)
        {
            Query
                .Include("_currentLoan")
                .Where(book => book.Id == bookId);
        }
    }
}