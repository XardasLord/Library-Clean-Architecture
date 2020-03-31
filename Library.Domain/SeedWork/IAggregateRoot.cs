using System.Collections.Generic;
using MediatR;

namespace Library.Domain.SeedWork
{
    public abstract class AggregateRoot<T> : Entity<T>
    {
        private List<INotification> _domainEvents;
        public IReadOnlyCollection<INotification> DomainEvents => _domainEvents?.AsReadOnly();

        public void AddDomainEvent(INotification eventItem)
        {
            _domainEvents ??= new List<INotification>();
            _domainEvents.Add(eventItem);
        }

        public void RemoveDomainEvent(INotification eventItem)
            => _domainEvents?.Remove(eventItem);

        public void ClearDomainEvents()
            => _domainEvents?.Clear();
    }
}
