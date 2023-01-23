using System.Reflection;
using DarkLink.Architecture.EventBus;
using DarkLink.Architecture.EventBus.Util;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace Microsoft.Extensions.DependencyInjection;

public static class DependencyInjection
{
    private static void AddEventProcessors(this IServiceCollection services, params Assembly[] assemblies)
    {
        if (assemblies.Length == 0)
            assemblies = AppDomain.CurrentDomain.GetAssemblies();

        foreach (var eventProcessorType in assemblies
                     .SelectMany(o => o.GetTypes())
                     .Where(o => o.IsConcrete() && o.Implements<IEventProcessor>()))
            services.AddSingleton(typeof(IEventProcessor), eventProcessorType);
    }

    public static void AddEvents(this IServiceCollection services)
    {
        services.TryAddSingleton<IEventBus, InMemoryEventBus>();
        services.AddSingleton(typeof(EventStream<>));
        services.AddSingleton<PublishEventDelegate>(sp => sp.GetRequiredService<IEventBus>().Publish);
        services.AddEventProcessors();
    }

    public static void UseEvents(this IServiceProvider services)
        => services.GetRequiredService<IEnumerable<IEventProcessor>>();
}
