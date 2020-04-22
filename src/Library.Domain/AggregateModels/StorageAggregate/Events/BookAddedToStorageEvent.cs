using System;
using MediatR;

namespace Library.Domain.AggregateModels.StorageAggregate.Events
{
    public class BookAddedToStorageEvent : INotification
    {
        public Book Book { get; }
        public DateTime DateOccured { get; }

        public BookAddedToStorageEvent(Book book, DateTime dateOccured)
        {
            Book = book;
            DateOccured = dateOccured;
        }
    }
}
