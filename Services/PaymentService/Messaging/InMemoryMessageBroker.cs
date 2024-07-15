namespace PaymentService.Messaging;

public class InMemoryMessageBroker : IMessageBroker
{
    private readonly Dictionary<Type, List<Func<object, Task>>> _handlers = new Dictionary<Type, List<Func<object, Task>>>();

    public async Task PublishAsync<TMessage>(TMessage message)
    {
        var messageType = typeof(TMessage);
        if (_handlers.ContainsKey(messageType))
        {
            var handlers = _handlers[messageType];
            foreach (var handler in handlers)
            {
                await handler(message);
            }
        }
    }

    public async Task SubscribeAsync<TMessage>(Func<TMessage, Task> handler)
    {
        var messageType = typeof(TMessage);
        if (!_handlers.ContainsKey(messageType))
        {
            _handlers[messageType] = new List<Func<object, Task>>();
        }
        _handlers[messageType].Add(async (message) => await handler((TMessage)message));
        await Task.CompletedTask;
    }
}
