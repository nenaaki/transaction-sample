using PaymentService.Events;
using PaymentService.EventStore;
using PaymentService.Commands;

namespace PaymentService.CommandHandlers;

public class CreatePaymentCommandHandler : ICommandHandler<CreatePaymentCommand>
{
    private readonly IEventStore _eventStore;

    public CreatePaymentCommandHandler(IEventStore eventStore)
    {
        _eventStore = eventStore;
    }

    public async Task HandleAsync(CreatePaymentCommand command)
    {
        var paymentCreatedEvent = new PaymentCreatedEvent(
            command.PaymentId, command.Amount, command.Currency, command.Description);
        await _eventStore.SaveAsync(paymentCreatedEvent);
    }
}
