using PaymentService.ReadModels;
using PaymentService.Events;

namespace PaymentService.QueryHandlers;

public interface IPaymentQueryHandler
{
    Task<PaymentReadModel> GetPaymentAsync(Guid paymentId);
    void Handle(PaymentCreatedEvent @event); // TODO: ここの依存関係は少し検討が必要かもしれません
}
