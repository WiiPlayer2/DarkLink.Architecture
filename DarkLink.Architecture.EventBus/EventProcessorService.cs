using Microsoft.Extensions.Hosting;

namespace DarkLink.Architecture.EventBus;

internal class EventProcessorService : IHostedService
{
    public EventProcessorService(IEnumerable<IEventProcessor> _) { }

    public Task StartAsync(CancellationToken cancellationToken) => Task.CompletedTask;

    public Task StopAsync(CancellationToken cancellationToken) => Task.CompletedTask;
}
