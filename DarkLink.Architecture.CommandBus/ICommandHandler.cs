namespace DarkLink.Architecture.CommandBus;

public interface ICommandHandler<in TCommand>
    where TCommand : Command
{
    Task ExecuteAsync(TCommand command);
}
