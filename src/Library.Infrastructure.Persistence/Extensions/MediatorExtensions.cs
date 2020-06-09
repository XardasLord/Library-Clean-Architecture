using System.Linq;
using System.Threading.Tasks;
using Library.Domain.SeedWork;
using Library.Infrastructure.Persistence.DbContexts;
using MediatR;

namespace Library.Infrastructure.Persistence.Extensions
{
    public static class MediatorExtensions
    {
        public static async Task DispatchDomainEventsAsync(this IMediator mediator, LibraryDbContext context)
        {
            var domainEntities = context.ChangeTracker
                .Entries<Entity>()
                .Where(x => x.Entity.DomainEvents != null && x.Entity.DomainEvents.Any())
                .ToList();

            var domainEvents = domainEntities
                .SelectMany(x => x.Entity.DomainEvents)
                .ToList();

            domainEntities
                .ForEach(entity => entity.Entity.ClearDomainEvents());

            // TODO: LoggedEvents table to easily see the history of events on Aggregates
            // https://app.pluralsight.com/course-player?clipId=f4eb2c3a-ee0e-44ee-a5d3-7c2f70862977

            var tasks = domainEvents
                .Select(async (domainEvent) => {
                    await mediator.Publish(domainEvent);
                });

            await Task.WhenAll(tasks);
        }
	}
}
