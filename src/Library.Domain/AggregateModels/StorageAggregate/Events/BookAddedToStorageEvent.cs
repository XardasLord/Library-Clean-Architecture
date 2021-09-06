using System;
using MediatR;

namespace Library.Domain.AggregateModels.StorageAggregate.Events
{
    public class BookAddedToStorageEvent : INotification
    {
        public Book Book { get; }
        public DateTime DateOccurred { get; }

        public BookAddedToStorageEvent(Book book, DateTime dateOccurred)
        {
            Book = book;
            DateOccurred = dateOccurred;
        }
    }
}
