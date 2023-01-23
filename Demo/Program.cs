using DarkLink.Architecture.EventBus;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

await Host.CreateDefaultBuilder(args)
    .ConfigureServices(services =>
    {
        services.AddEvents();

        services.AddHostedService<Demo>();
        services.AddSingleton<Demo2>();
    })
    .Build()
    .RunAsync();

internal class Demo : BackgroundService
{
    private readonly PublishEventDelegate publishEvent;

    public Demo(PublishEventDelegate publishEvent, Demo2 _)
    {
        this.publishEvent = publishEvent;
    }

    protected override Task ExecuteAsync(CancellationToken stoppingToken)
    {
        publishEvent("hi");
        return Task.CompletedTask;
    }
}

internal class Demo2 : IDisposable
{
    private readonly IDisposable subscription;

    public Demo2(EventStream<string> stringEvents, ILogger<Demo2> logger)
    {
        subscription = stringEvents
            .Subscribe(@string => logger.LogInformation("String: {string}", @string));
    }

    public void Dispose()
    {
        subscription.Dispose();
    }
}
