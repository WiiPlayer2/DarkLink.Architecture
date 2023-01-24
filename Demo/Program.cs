using DarkLink.Architecture.CommandBus;
using DarkLink.Architecture.EventBus;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

await Host.CreateDefaultBuilder(args)
    .ConfigureServices(services =>
    {
        services.AddEvents();
        services.AddCommands();

        services.AddHostedService<Demo>();
    })
    .Build()
    .RunAsync();

internal record DemoCommand : Command;

internal class Demo : BackgroundService
{
    private readonly InvokeCommandDelegate invokeCommand;

    private readonly ILogger<Demo> logger;

    private readonly PublishEventDelegate publishEvent;

    public Demo(
        PublishEventDelegate publishEvent,
        InvokeCommandDelegate invokeCommand,
        ILogger<Demo> logger)
    {
        this.publishEvent = publishEvent;
        this.invokeCommand = invokeCommand;
        this.logger = logger;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        publishEvent("hi");
        await invokeCommand(new DemoCommand());
        logger.LogInformation("Demo end.");
    }
}

internal class DemoEventProcessor : IDisposable, IEventProcessor
{
    private readonly IDisposable subscription;

    public DemoEventProcessor(EventStream<string> stringEvents, ILogger<DemoEventProcessor> logger)
    {
        subscription = stringEvents
            .Subscribe(@string => logger.LogInformation("String: {string}", @string));
    }

    public void Dispose()
    {
        subscription.Dispose();
    }
}

internal class DemoCommandHandler : ISyncCommandHandler<DemoCommand>
{
    private readonly ILogger<DemoCommand> logger;

    public DemoCommandHandler(ILogger<DemoCommand> logger)
    {
        this.logger = logger;
    }

    public void Execute(DemoCommand command) => logger.LogInformation("Command!");
}
