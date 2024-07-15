using PaymentService.Events;

namespace PaymentService.EventStore;

public interface IEventStore
{
    Task SaveAsync(IEvent @event);
    Task<IEnumerable<IEvent>> LoadAsync(Guid aggregateId);
}
