namespace PaymentService.ReadModels;

public class PaymentReadModel
{
    public Guid PaymentId { get; set; }
    public decimal Amount { get; set; }
    public string Currency { get; set; }
    public string Description { get; set; }
    public DateTime CreatedAt { get; set; }
}
