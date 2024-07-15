using PaymentService.Events;
using PaymentService.QueryHandlers;

namespace PaymentService.EventHandlers;

public class PaymentCreatedEventHandler : IEventHandler<PaymentCreatedEvent>
{
    private readonly IPaymentQueryHandler _queryHandler;

    public PaymentCreatedEventHandler(IPaymentQueryHandler queryHandler)
    {
        _queryHandler = queryHandler;
    }

    public async Task HandleAsync(PaymentCreatedEvent @event)
    {
        _queryHandler.Handle(@event);
        await Task.CompletedTask;
    }
}