namespace DarkLink.Architecture.CommandBus;

public interface ISyncCommandHandler<in TCommand> : ICommandHandler<TCommand>
    where TCommand : Command
{
    Task ICommandHandler<TCommand>.ExecuteAsync(TCommand command)
    {
        Execute(command);
        return Task.CompletedTask;
    }

    void Execute(TCommand command);
}
