using System.Reflection;
using DarkLink.Architecture.CommandBus;
using DarkLink.Architecture.EventBus.Util;

namespace Microsoft.Extensions.DependencyInjection;

public static class DependencyInjection
{
    public static void AddCommands(this IServiceCollection services, params Assembly[] assemblies)
    {
        if (assemblies.Length == 0)
            assemblies = AppDomain.CurrentDomain.GetAssemblies();

        services.AddSingleton<CommandExecutor>();
        services.AddSingleton<InvokeCommandDelegate>(sp => sp.GetRequiredService<CommandExecutor>().ExecuteCommandAsync);
        foreach (var commandHandlerType in assemblies
                     .SelectMany(o => o.GetTypes())
                     .Where(t => t.IsConcrete() && t.Implements(typeof(ICommandHandler<>))))
            services.AddSingleton(commandHandlerType.GetImplementedType(typeof(ICommandHandler<>)), commandHandlerType);
    }
}
