namespace PaymentService.Commands;

public class CreatePaymentCommand : ICommand
{
    public Guid PaymentId { get; }
    public decimal Amount { get; }
    public string Currency { get; }
    public string Description { get; }

    public CreatePaymentCommand(Guid paymentId, decimal amount, string currency, string description)
    {
        PaymentId = paymentId;
        Amount = amount;
        Currency = currency;
        Description = description;
    }
}
