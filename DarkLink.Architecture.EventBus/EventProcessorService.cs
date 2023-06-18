using DarkLink.Architecture.EventBus.Events;
using Microsoft.Extensions.Hosting;

namespace DarkLink.Architecture.EventBus;

internal class EventProcessorService : IHostedService
{
    private readonly PublishEventDelegate publishEvent;

    public EventProcessorService(IEnumerable<IEventProcessor> _, PublishEventDelegate publishEvent)
    {
        this.publishEvent = publishEvent;
    }

    public Task StartAsync(CancellationToken cancellationToken)
    {
        publishEvent(new ApplicationStarting());
        return Task.CompletedTask;
    }

    public Task StopAsync(CancellationToken cancellationToken)
    {
        publishEvent(new ApplicationStopping());
        return Task.CompletedTask;
    }
}
