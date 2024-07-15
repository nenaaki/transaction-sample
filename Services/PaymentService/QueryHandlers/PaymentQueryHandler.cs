using PaymentService.ReadModels;
using PaymentService.Events;

namespace PaymentService.QueryHandlers;

public class PaymentQueryHandler : IPaymentQueryHandler
{
    private readonly List<PaymentReadModel> _payments = new List<PaymentReadModel>();

    public async Task<PaymentReadModel> GetPaymentAsync(Guid paymentId)
    {
        var payment = _payments.SingleOrDefault(p => p.PaymentId == paymentId);
        return await Task.FromResult(payment);
    }

    public void Handle(PaymentCreatedEvent @event)
    {
        var payment = new PaymentReadModel
        {
            PaymentId = @event.PaymentId,
            Amount = @event.Amount,
            Currency = @event.Currency,
            Description = @event.Description,
            CreatedAt = @event.OccurredOn
        };
        _payments.Add(payment);
    }
}