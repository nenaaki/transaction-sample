namespace PaymentService.Messaging;

public interface IMessageBroker
{
    Task PublishAsync<TMessage>(TMessage message);
    Task SubscribeAsync<TMessage>(Func<TMessage, Task> handler);
}
