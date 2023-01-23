using DarkLink.Architecture.EventBus;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace Microsoft.Extensions.DependencyInjection;

public static class DependencyInjection
{
    public static void AddEvents(this IServiceCollection services)
    {
        services.TryAddSingleton<IEventBus, InMemoryEventBus>();
        services.AddSingleton(typeof(EventStream<>));
        services.AddSingleton<PublishEventDelegate>(sp => sp.GetRequiredService<IEventBus>().Publish);
    }
}
