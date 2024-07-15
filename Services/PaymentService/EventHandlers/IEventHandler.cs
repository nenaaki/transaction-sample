using PaymentService.Events;

namespace PaymentService.EventHandlers;

public interface IEventHandler<TEvent> where TEvent : IEvent
{
    Task HandleAsync(TEvent @event);
}