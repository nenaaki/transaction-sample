namespace PaymentService.Events;

public class PaymentCreatedEvent : IEvent
{
    public Guid Id { get; }
    public DateTime OccurredOn { get; }
    public Guid PaymentId { get; }
    public decimal Amount { get; }
    public string Currency { get; }
    public string Description { get; }

    public PaymentCreatedEvent(Guid paymentId, decimal amount, string currency, string description)
    {
        Id = Guid.NewGuid();
        OccurredOn = DateTime.UtcNow;
        PaymentId = paymentId;
        Amount = amount;
        Currency = currency;
        Description = description;
    }
}