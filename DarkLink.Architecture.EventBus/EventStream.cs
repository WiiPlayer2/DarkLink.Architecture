using System.Reactive.Linq;

namespace DarkLink.Architecture.EventBus;

public class EventStream<T> : IObservable<T>
{
    private readonly IObservable<T> source;

    public EventStream(IEventBus eventBus)
    {
        source = eventBus.Events.OfType<T>();
    }

    public IDisposable Subscribe(IObserver<T> observer) => source.Subscribe(observer);
}
