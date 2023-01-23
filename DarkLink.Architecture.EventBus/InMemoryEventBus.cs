using System.Reactive.Subjects;
using Microsoft.Extensions.Logging;

namespace DarkLink.Architecture.EventBus;

public class InMemoryEventBus : IEventBus
{
    private readonly ILogger<InMemoryEventBus> logger;

    private readonly Subject<object> subject = new();

    public InMemoryEventBus(ILogger<InMemoryEventBus> logger)
    {
        this.logger = logger;
    }

    public IObservable<object> Events => subject;

    public void Publish(object @event)
    {
        logger.LogTrace("Publishing {event}...", @event);
        subject.OnNext(@event);
    }
}
