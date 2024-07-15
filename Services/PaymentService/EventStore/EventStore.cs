using PaymentService.Events;

namespace PaymentService.EventStore;

public class EventStore : IEventStore
{
    private readonly List<IEvent> _events = new List<IEvent>();

    public async Task SaveAsync(IEvent @event)
    {
        // 永続化ロジックを実装（例：データベースに保存）
        _events.Add(@event);
        await Task.CompletedTask;
    }

    public async Task<IEnumerable<IEvent>> LoadAsync(Guid aggregateId)
    {
        // ロードロジックを実装（例：データベースから読み込む）
        return await Task.FromResult(_events.Where(e => e.Id == aggregateId));
    }
}