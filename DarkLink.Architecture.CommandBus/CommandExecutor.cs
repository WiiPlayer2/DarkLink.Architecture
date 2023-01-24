using System.Reflection;
using Microsoft.Extensions.DependencyInjection;

namespace DarkLink.Architecture.CommandBus;

public class CommandExecutor
{
    private readonly IServiceProvider serviceProvider;

    public CommandExecutor(IServiceProvider serviceProvider)
    {
        this.serviceProvider = serviceProvider;
    }

    public Task ExecuteCommandAsync(Command command)
    {
        var commandType = command.GetType();
        var runMethod = typeof(CommandExecutor).GetMethod(nameof(RunCommandAsync), BindingFlags.NonPublic | BindingFlags.Instance)!.MakeGenericMethod(commandType);
        return (Task) runMethod.Invoke(this, new[] {command,})!;
    }

    private Task RunCommandAsync<TCommand>(TCommand command)
        where TCommand : Command
        => serviceProvider.GetRequiredService<ICommandHandler<TCommand>>().ExecuteAsync(command);
}
